using StudyApi.Business.Models;

namespace StudyApi.Business.Interfaces;
public interface IAdressRepository : IRepository<Adress>
{
    Task<Adress> GetAdressBySupplier(Guid supplierId);
}
