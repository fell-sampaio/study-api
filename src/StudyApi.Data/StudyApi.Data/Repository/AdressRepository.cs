using Microsoft.EntityFrameworkCore;
using StudyApi.Business.Interfaces;
using StudyApi.Business.Models;
using StudyApi.Data.Context;

namespace StudyApi.Data.Repository;
public class AdressRepository(AppDbContext dbContext) : Repository<Adress>(dbContext), IAdressRepository
{
    public async Task<Adress> GetAdressBySupplier(Guid supplierId)
    {
        return await dbContext.Adresses.AsNoTracking()
                                       .FirstOrDefaultAsync(f => f.SupplierId == supplierId);
    }
}
