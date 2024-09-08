using Microsoft.AspNetCore.Mvc.ModelBinding;
using StudyApi.Api.DTOs;
using System.Text.Json;

namespace StudyApi.Api.Extensions;

public class ProductModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        JsonSerializerOptions jsonSerializerOptions = new()
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true
        };
        JsonSerializerOptions serializeOptions = jsonSerializerOptions;

        var productValueProviderResult = bindingContext.ValueProvider.GetValue("product");
        if (productValueProviderResult != ValueProviderResult.None)
        {
            var productString = productValueProviderResult.FirstValue;
            var produtoImagemViewModel = JsonSerializer.Deserialize<ProductImageDto>(productString, serializeOptions);

            if (bindingContext.ActionContext.HttpContext.Request.Form.Files.Count > 0)
            {
                produtoImagemViewModel.ImageUpload = bindingContext.ActionContext.HttpContext.Request.Form.Files[0];
            }

            bindingContext.Result = ModelBindingResult.Success(produtoImagemViewModel);
        }
        else
        {
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}
