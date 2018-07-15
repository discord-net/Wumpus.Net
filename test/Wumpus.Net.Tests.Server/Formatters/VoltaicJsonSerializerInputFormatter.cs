using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Buffers;
using System.Text;
using System.Threading.Tasks;
using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Server.Formatters
{
    public class VoltaicJsonSerializerInputFormatter : TextInputFormatter
    {
        private readonly JsonSerializer _serializer;
        private readonly ArrayPool<byte> _pool;

        public VoltaicJsonSerializerInputFormatter(ConverterCollection converters = null, ArrayPool<byte> pool = null)
            : this(new JsonSerializer(converters, pool), pool) { }
        public VoltaicJsonSerializerInputFormatter(JsonSerializer serializer, ArrayPool<byte> pool = null)
        {
            _serializer = serializer;
            _pool = pool ?? ArrayPool<byte>.Shared;

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
            SupportedMediaTypes.Add("application/json");
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));

            var request = context.HttpContext.Request;
            var buffer = new ResizableMemory<byte>(1024 * 4);
            try
            {
                var read = await request.Body.ReadAsync(buffer.GetMemory(1024 * 4));
                while (read > 0)
                {
                    buffer.Advance(read);
                    read = await request.Body.ReadAsync(buffer.GetMemory(1024 * 4));
                }
                
                    if (encoding == Encoding.UTF8)
                    {
                        var obj = _serializer.ReadUtf8(context.ModelType, buffer.AsReadOnlySpan());
                        return InputFormatterResult.Success(obj);
                    }
                    else if (encoding == Encoding.Unicode)
                    {
                        var obj = _serializer.ReadUtf16(context.ModelType, buffer.AsReadOnlySpan());
                        return InputFormatterResult.Success(obj);
                    }
                    else
                        throw new ArgumentOutOfRangeException(nameof(encoding));
            }
            catch (Exception ex)
            {
                throw new InputFormatterException($"Failed to deserialize {context.ModelType}", ex);
            }
            finally { buffer.Return(); }
        }
    }
}
