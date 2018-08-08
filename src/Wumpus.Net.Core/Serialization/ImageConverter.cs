using System;
using System.IO;
using System.Reflection;
using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Serialization
{
    public class ImageConverter : ValueConverter<Image>
    {
        private readonly ValueConverter<Utf8String> _hashConverter;
        private readonly ValueConverter<string> _base64Converter;

        public ImageConverter(Serializer serializer, PropertyInfo propInfo)
        {
            _hashConverter = serializer.GetConverter<Utf8String>(propInfo, true);
            _base64Converter = serializer.GetConverter<string>(propInfo, true);
        }

        public override bool CanWrite(Image value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value.Hash != (Utf8String)default || value.Stream != default;

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out Image result, PropertyMap propMap = null)
        {
            if (!_hashConverter.TryRead(ref remaining, out var hashValue, propMap))
            {
                result = default;
                return false;
            }
            result = new Image(hashValue);
            return true;
        }

        public override bool TryWrite(ref ResizableMemory<byte> remaining, Image value, PropertyMap propMap = null)
        {
            if (!(value.Hash is null))
                return _hashConverter.TryWrite(ref remaining, value.Hash, propMap);
            if (value.Stream != null)
            {
                // TODO: Should use pooling and/or ResizableMemory
                byte[] bytes;
                int length;
                if (value.Stream.CanSeek)
                {
                    bytes = new byte[value.Stream.Length - value.Stream.Position];
                    length = value.Stream.Read(bytes, 0, bytes.Length);
                }
                else
                {
                    var tempStream = new MemoryStream();
                    value.Stream.CopyTo(tempStream);
                    if (!tempStream.TryGetBuffer(out var arrSegment))
                        return false;
                    bytes = arrSegment.Array;
                    length = arrSegment.Count;
                }

                // TODO: Use UTF8 strings
                string base64 = Convert.ToBase64String(bytes, 0, length);
                string str;
                switch (value.StreamFormat)
                {
                    case ImageFormat.Jpeg: str = $"data:image/jpeg;base64,{base64}"; break;
                    case ImageFormat.Png: str = $"data:image/png;base64,{base64}"; break;
                    case ImageFormat.Gif: str = $"data:image/gif;base64,{base64}"; break;
                    case ImageFormat.WebP: str = $"data:image/webp;base64,{base64}"; break;
                    default:
                        return false;
                }
                return _base64Converter.TryWrite(ref remaining, str, propMap);
            }
            return false;
        }
    }
}
