using StudyApi.Business.Models;

namespace StudyApi.Business.Interfaces;
public interface ISupplierRepository : IRepository<Supplier>
{
    Task<Supplier> GetSupplierAdress(Guid id);
    Task<Supplier> GetSupplierProductsAdress(Guid id);
}
