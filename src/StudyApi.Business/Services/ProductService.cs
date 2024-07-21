using StudyApi.Business.Interfaces;
using StudyApi.Business.Models;
using StudyApi.Business.Models.Validations;

namespace StudyApi.Business.Services;
public class ProductService(IProductRepository productRepository,
    INotifier notifier) : BaseService(notifier), IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task Add(Product product)
    {
        if (!ExecuteValidation(new ProductValidation(), product)) return;
        await _productRepository.Add(product);
    }

    public async Task Update(Product product)
    {
        if (!ExecuteValidation(new ProductValidation(), product)) return;

        await _productRepository.Update(product);
    }

    public async Task Delete(Guid id)
    {
        await _productRepository.Delete(id);
    }

    public void Dispose()
    {
        _productRepository?.Dispose();
    }
}
