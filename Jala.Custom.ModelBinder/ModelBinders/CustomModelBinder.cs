using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Jala.Custom.ModelBinder.ModelBinders
{
    public class CustomModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            //if( bindingContext.ActionContext.HttpContext.Request.Query.Count > 0)
            //{
            //    var id = bindingContext.ActionContext.HttpContext.Request.Query["id"];
            //    var page = new Page()
            //    {
            //        Id = int.Parse(id),
            //        Name = ""
            //    };
            //    bindingContext.Result = ModelBindingResult.Success(page);
            //    return Task.CompletedTask;
            //}
            //return Task.CompletedTask;

            string valueFromBody;

            using (var streamReader = new StreamReader(bindingContext.HttpContext.Request.Body))
            {
                valueFromBody = await streamReader.ReadToEndAsync();
            }

            if (string.IsNullOrWhiteSpace(valueFromBody))
                return;

            Page modelInstance = null;
            try
            {
                modelInstance = JsonConvert.DeserializeObject<Page>(valueFromBody);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            bindingContext.Result = ModelBindingResult.Success(modelInstance);
        }
    }
}

