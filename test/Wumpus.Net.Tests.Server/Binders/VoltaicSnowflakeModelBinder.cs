using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace Wumpus.Server.Binders
{
    public class VoltaicSnowflakeModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var modelName = bindingContext.ModelMetadata.ParameterName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(value))
                return Task.CompletedTask;

            if (!ulong.TryParse(value, out ulong id))
            {
                bindingContext.ModelState.TryAddModelError(
                                        bindingContext.ModelName,
                                        "Snowflake must be an integer.");
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(new Snowflake(id));
            return Task.CompletedTask;
        }
    }
}
