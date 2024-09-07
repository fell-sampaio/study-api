using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudyApi.Api.DTOs;
using StudyApi.Business.Interfaces;
using StudyApi.Business.Models;

namespace StudyApi.Api.Controllers;

[Route("api/products")]
public class ProductsController(INotifier notifier, IProductRepository productRepository, IProductService productService, IMapper mapper) : MainController(notifier)
{
    [HttpGet]
    public async Task<IEnumerable<ProductDto>> GetAll()
    {
        return mapper.Map<IEnumerable<ProductDto>>(await productRepository.GetSuppliersProducts());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDto>> GetById(Guid id)
    {
        var productDto = await GetProduct(id);

        if (productDto == null) return NotFound();

        return productDto;
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Add(ProductDto productDto)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var imageName = Guid.NewGuid() + "_" + productDto.Image;
        if (!FileUpload(productDto.ImageUpload, imageName)) return CustomResponse(productDto);

        productDto.Image = imageName;
        await productService.Add(mapper.Map<Product>(productDto));

        return CustomResponse(productDto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ProductDto>> Delete(Guid id)
    {
        var product = await GetProduct(id);

        if (product == null) return NotFound();

        await productService.Delete(id);

        return CustomResponse(product);
    }

    private bool FileUpload(string file, string imgName)
    {
        if (string.IsNullOrEmpty(file))
        {
            NotifyError("Provide an image for this product!");
            return false;
        }

        var imageDataByteArray = Convert.FromBase64String(file);

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgName);

        if (System.IO.File.Exists(filePath))
        {
            NotifyError("There is already a file with this name!");
            return false;
        }

        System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

        return true;
    }

    private async Task<ProductDto> GetProduct(Guid id)
    {
        return mapper.Map<ProductDto>(await productRepository.GetSupplierProduct(id));
    }
}
