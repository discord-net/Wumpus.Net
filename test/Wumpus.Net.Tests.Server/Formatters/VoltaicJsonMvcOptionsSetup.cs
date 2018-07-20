using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Wumpus.Server.Binders;

namespace Wumpus.Server.Formatters
{
    public class VoltaicJsonMvcOptionsSetup : IConfigureOptions<MvcOptions>
    {
        private readonly SerializerOptions _serializerOptions;

        public VoltaicJsonMvcOptionsSetup(SerializerOptions serializerOptions)
        {
            _serializerOptions = serializerOptions;
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
            options.OutputFormatters.Add(new VoltaicJsonOutputFormatter(_serializerOptions.Serializer, _serializerOptions.Pool));
            options.InputFormatters.Add(new VoltaicJsonInputFormatter(_serializerOptions.Serializer, _serializerOptions.Pool));

            // Model binders
            options.ModelBinderProviders.Insert(0, new VoltaicModelBinderProvider());
        }
    }
}
