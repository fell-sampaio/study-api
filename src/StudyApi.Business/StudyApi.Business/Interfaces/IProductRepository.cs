using StudyApi.Business.Models;

namespace StudyApi.Business.Interfaces;
public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsBySupplier(Guid supplierId);
    Task<IEnumerable<Product>> GetSuppliersProducts();
    Task<Product> GetSupplierProduct(Guid id);
}
