using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Jala.Custom.ModelBinder.ModelBinders
{
    public class CustomModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if(context.Metadata.ModelType == typeof(Page))
            {
                return new CustomModelBinder();
            }
            return null;
        }
    }
}
