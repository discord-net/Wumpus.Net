using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Buffers;
using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Server.Formatters
{
    public class VoltaicJsonSerializerMvcOptionsSetup : IConfigureOptions<MvcOptions>
    {
        private readonly JsonSerializer _serializer;
        private readonly ArrayPool<byte> _pool;

        public VoltaicJsonSerializerMvcOptionsSetup(ConverterCollection converters = null, ArrayPool<byte> pool = null)
            : this(new JsonSerializer(converters, pool), pool) { }
        public VoltaicJsonSerializerMvcOptionsSetup(JsonSerializer serializer, ArrayPool<byte> pool = null)
        {
            _serializer = serializer;
            _pool = pool;
        }

        public void Configure(MvcOptions options)
        {
            const string key = "json";
            var mapping = options.FormatterMappings.GetMediaTypeMappingForFormat(key);
            if (string.IsNullOrEmpty(mapping))
                options.FormatterMappings.SetMediaTypeMappingForFormat(key, "application/json");

            options.OutputFormatters.Add(new VoltaicJsonSerializerOutputFormatter(_serializer, _pool));
            options.InputFormatters.Add(new VoltaicJsonSerializerInputFormatter(_serializer, _pool));
        }
    }
}
