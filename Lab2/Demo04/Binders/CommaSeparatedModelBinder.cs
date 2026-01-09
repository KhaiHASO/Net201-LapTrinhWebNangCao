using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo04.Binders
{
    public class CommaSeparatedModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            // 1. Get the ValueProvider result for the model name
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            // 2. Set the model state value
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            // 3. Get the actual value string
            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            // 4. Custom Logic: Split by comma and parse to List<int>
            try
            {
                var result = value.Split(',')
                                  .Select(x => int.Parse(x.Trim()))
                                  .ToList();

                bindingContext.Result = ModelBindingResult.Success(result);
            }
            catch (Exception)
            {
                // If parsing fails, adding error to model state
                bindingContext.ModelState.TryAddModelError(
                    bindingContext.ModelName, "Must be a comma-separated list of integers.");
            }

            // Custom binders run synchronously in this logic, but API depends on Task return.
            // Task.CompletedTask indicates the async operation is finished.
            return Task.CompletedTask;
        }
    }
}
