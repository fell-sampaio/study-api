using Microsoft.EntityFrameworkCore;
using StudyApi.Business.Interfaces;
using StudyApi.Business.Models;
using StudyApi.Data.Context;

namespace StudyApi.Data.Repository;
public class SupplierRepository(AppDbContext context) : Repository<Supplier>(context), ISupplierRepository
{
    public async Task<Supplier> GetSupplierAdress(Guid id)
    {
        return await _dbContext.Suppliers.AsNoTracking()
            .Include(c => c.Adress)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Supplier> GetSupplierProductsAdress(Guid id)
    {
        return await _dbContext.Suppliers.AsNoTracking()
            .Include(c => c.Products)
            .Include(c => c.Adress)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
