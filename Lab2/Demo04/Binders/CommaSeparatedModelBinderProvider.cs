using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Collections.Generic;

namespace Demo04.Binders
{
    public class CommaSeparatedModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Check if the target usage is of type List<int>
            if (context.Metadata.ModelType == typeof(List<int>))
            {
                // Return our custom binder for List<int>
                return new BinderTypeModelBinder(typeof(CommaSeparatedModelBinder));
            }

            return null;
        }
    }
}
