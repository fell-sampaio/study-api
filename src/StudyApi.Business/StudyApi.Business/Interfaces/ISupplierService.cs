using StudyApi.Business.Models;

namespace StudyApi.Business.Interfaces;
public interface ISupplierService : IDisposable
{
    Task<bool> Add(Supplier supplier);
    Task<bool> Update(Supplier supplier);
    Task<bool> Delete(Guid id);
    Task UpdateAdress(Adress adress);
}
