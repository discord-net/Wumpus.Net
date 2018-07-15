using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Buffers;
using System.Text;
using System.Threading.Tasks;
using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Server.Formatters
{
    public class VoltaicJsonSerializerOutputFormatter : TextOutputFormatter
    {
        private readonly JsonSerializer _serializer;

        public VoltaicJsonSerializerOutputFormatter(ConverterCollection converters = null, ArrayPool<byte> pool = null)
            : this(new JsonSerializer(converters, pool), pool) { }
        public VoltaicJsonSerializerOutputFormatter(JsonSerializer serializer, ArrayPool<byte> pool = null)
        {
            _serializer = serializer;

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
            SupportedMediaTypes.Add("application/json");
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding encoding)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));

            if (encoding == Encoding.UTF8)
            {
                var bytes = _serializer.WriteUtf8(context.Object);
                await context.HttpContext.Response.Body.WriteAsync(bytes);
            }
            else if (encoding == Encoding.Unicode)
            {
                var bytes = _serializer.WriteUtf16Bytes(context.Object);
                await context.HttpContext.Response.Body.WriteAsync(bytes);
            }
            else
                throw new ArgumentOutOfRangeException(nameof(encoding));
        }
    }
}
