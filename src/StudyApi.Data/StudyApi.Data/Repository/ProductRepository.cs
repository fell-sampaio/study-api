using Microsoft.EntityFrameworkCore;
using StudyApi.Business.Interfaces;
using StudyApi.Business.Models;
using StudyApi.Data.Context;

namespace StudyApi.Data.Repository;
public class ProductRepository(AppDbContext dbContext) : Repository<Product>(dbContext), IProductRepository
{
    public async Task<Product> GetSupplierProduct(Guid id)
    {
        return await _dbContext.Products.AsNoTracking().Include(f => f.Supplier)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetSuppliersProducts()
    {
        return await _dbContext.Products.AsNoTracking().Include(f => f.Supplier)
            .OrderBy(p => p.Name).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsBySupplier(Guid supplierId)
    {
        return await Search(p => p.SupplierId == supplierId);
    }
}
