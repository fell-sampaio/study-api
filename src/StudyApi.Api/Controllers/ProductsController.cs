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

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ProductDto productDto)
    {
        if (id != productDto.Id)
        {
            NotifyError("Product not found!");
            return CustomResponse();
        }

        var productUpdate = await GetProduct(id);
        productDto.Image = productUpdate.Image;
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        if (productDto.ImageUpload != null)
        {
            var imageName = Guid.NewGuid() + "_" + productDto.Image;
            if (!FileUpload(productDto.ImageUpload, imageName))
            {
                return CustomResponse(ModelState);
            }

            productUpdate.Image = imageName;
        }

        productUpdate.Name = productDto.Name;
        productDto.SupplierId = productDto.SupplierId;
        productUpdate.Description = productDto.Description;
        productUpdate.Value = productDto.Value;
        productUpdate.Active = productDto.Active;

        await productService.Update(mapper.Map<Product>(productUpdate));

        return CustomResponse(productDto);
    }

    [HttpPost("Add")]
    public async Task<ActionResult<ProductDto>> AddAlternative(ProductImageDto productDto)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var imagePrefix = Guid.NewGuid() + "_";
        if (!await AlternativeFileUpload(productDto.ImageUpload, imagePrefix)) return CustomResponse(ModelState);

        productDto.Image = imagePrefix + productDto.ImageUpload.FileName;
        await productService.Add(mapper.Map<Product>(productDto));

        return CustomResponse(productDto);
    }

    [RequestSizeLimit(40000000)] // Use this in IFormFile and not in strings
    [HttpPost("image")]
    public ActionResult<ProductDto> AddImage(IFormFile file)
    {
        return Ok(file);
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

    private async Task<bool> AlternativeFileUpload(IFormFile file, string imgPrefix)
    {
        if (file == null || file.Length <= 0)
        {
            ModelState.AddModelError(string.Empty, "Provide an image for this product!");
            return false;
        }

        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefix + file.FileName);

        if (System.IO.File.Exists(path))
        {
            ModelState.AddModelError(string.Empty, "There is already a file with this name!");
            return false;
        }

        using var stream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream);

        return true;
    }

    private async Task<ProductDto> GetProduct(Guid id)
    {
        return mapper.Map<ProductDto>(await productRepository.GetSupplierProduct(id));
    }
}
