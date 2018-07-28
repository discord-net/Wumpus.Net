using RestEase;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Voltaic.Serialization;
using Wumpus.Requests;
using Wumpus.Serialization;

namespace Wumpus.Net
{
    internal class WumpusBodySerializer : RequestBodySerializer
    {
        private readonly WumpusJsonSerializer _serializer;

        public WumpusBodySerializer(WumpusJsonSerializer serializer)
        {
            _serializer = serializer;
        }

        public override HttpContent SerializeBody<T>(T body, RequestBodySerializerInfo info)
        {
            if (body == null)
                return null;

            if (body is IFormData form)
            {
                var pairs = form.GetFormData();
                var content = new MultipartFormDataContent();
                foreach (var pair in pairs)
                {
                    if (pair.Value is MultipartFile file)
                    {
                        var stream = file.Stream;
                        if (stream.CanSeek)
                        {
                            long remaining = stream.Length - stream.Position;
                            if (remaining > int.MaxValue)
                                throw new InvalidOperationException("Uploading files larger than Int32.MaxValue bytes is unsupported");
                            else if (remaining <= 0)
                            {
                                content.Add(new ByteArrayContent(Array.Empty<byte>()), pair.Key, (string)file.Filename);
                                continue;
                            }
                            var arr = new byte[remaining];
                            stream.Read(arr, 0, arr.Length);
                            content.Add(new ByteArrayContent(arr), pair.Key, (string)file.Filename);
                        }
                        else
                        {
                            var buffer = new ResizableMemory<byte>(4096); // 4 KB
                            while (true)
                            {
                                var segment = buffer.GetSegment(4096);
                                int bytesCopied = file.Stream.Read(segment.Array, segment.Offset, segment.Count);
                                if (bytesCopied == 0)
                                    break;
                                buffer.Advance(bytesCopied);
                            }
                            content.Add(new ByteArrayContent(buffer.ToArray()), pair.Key, (string)file.Filename);
                        }
                    }
                    else
                        content.Add(new StringContent(_serializer.WriteUtf16String(pair.Value), Encoding.UTF8, "application/json"), pair.Key);
                }
                return content;
            }
            else
            {
                var arr = _serializer.Write(body).AsSegment();
                var content = new ByteArrayContent(arr.Array, arr.Offset, arr.Count);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return content;
            }
        }
    }
}
