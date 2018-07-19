using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using System.Buffers;
using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Server.Formatters
{
    public class VoltaicJsonMvcOptionsSetup : IConfigureOptions<MvcOptions>
    {
        private readonly JsonSerializer _serializer;
        private readonly ArrayPool<byte> _pool;

        public VoltaicJsonMvcOptionsSetup(ConverterCollection converters = null, ArrayPool<byte> pool = null)
            : this(new JsonSerializer(converters, pool), pool) { }
        public VoltaicJsonMvcOptionsSetup(JsonSerializer serializer, ArrayPool<byte> pool = null)
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

            // Disable Newtonsoft.Json
            options.OutputFormatters.RemoveType<JsonOutputFormatter>();
            options.InputFormatters.RemoveType<JsonInputFormatter>();

            // Add our serializer
            options.OutputFormatters.Add(new VoltaicJsonOutputFormatter(_serializer, _pool));
            options.InputFormatters.Add(new VoltaicJsonInputFormatter(_serializer, _pool));
        }
    }
}
