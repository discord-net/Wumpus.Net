using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Linq;
using Voltaic;
using Wumpus.Requests;

namespace Wumpus.Server.Binders
{
    public class VoltaicModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Metadata.ModelType == typeof(Snowflake))
                return new BinderTypeModelBinder(typeof(VoltaicSnowflakeModelBinder));
            if (context.Metadata.ModelType == typeof(Utf8String))
                return new BinderTypeModelBinder(typeof(VoltaicUtf8StringModelBinder));
            if (context.Metadata.ModelType.GetInterfaces().Any(x => x == typeof(IFormData)))
                return new BinderTypeModelBinder(typeof(VoltaicFormDataModelBinder));

            return null;
        }
    }
}
