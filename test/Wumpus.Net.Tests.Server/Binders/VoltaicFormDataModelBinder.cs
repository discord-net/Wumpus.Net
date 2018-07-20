using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace Wumpus.Server.Binders
{
    public class VoltaicFormDataModelBinder : IModelBinder
    {
        private readonly SerializerOptions _options;
        public VoltaicFormDataModelBinder(SerializerOptions options)
        {
            _options = options;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var modelName = bindingContext.FieldName ?? bindingContext.ModelMetadata.ParameterName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None)
                return;

            var values = await GetFormValuesAsync(modelName, bindingContext).ConfigureAwait(false);
            if (values.Count == 0)
                return;

            var value = _options.Serializer.ReadUtf16(bindingContext.ModelType, values[0]);
            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);
            bindingContext.Result = ModelBindingResult.Success(value);
        }

        private async Task<StringValues> GetFormValuesAsync(string modelName, ModelBindingContext bindingContext)
        {
            var request = bindingContext.HttpContext.Request;
            if (request.HasFormContentType)
            {
                var form = await request.ReadFormAsync().ConfigureAwait(false);
                if (form.TryGetValue(modelName, out var values))
                    return values;
            }
            return StringValues.Empty;
        }
    }
}
