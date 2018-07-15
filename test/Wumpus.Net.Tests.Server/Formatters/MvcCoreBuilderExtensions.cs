using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Buffers;
using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Server.Formatters
{
    public static class MvcCoreBuilderExtensions
    {
        public static IMvcCoreBuilder AddVoltaicJsonSerializerFormatters(this IMvcCoreBuilder builder, ConverterCollection converters = null, ArrayPool<byte> pool = null)
            => AddVoltaicJsonSerializerFormatters(builder, new JsonSerializer(converters, pool), pool);
        public static IMvcCoreBuilder AddVoltaicJsonSerializerFormatters(this IMvcCoreBuilder builder, JsonSerializer serializer, ArrayPool<byte> pool = null)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, VoltaicJsonSerializerMvcOptionsSetup>(_ => new VoltaicJsonSerializerMvcOptionsSetup(serializer, pool)));
            return builder;
        }
    }
}
