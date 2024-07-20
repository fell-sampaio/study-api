using StudyApi.Business.Models;

namespace StudyApi.Business.Interfaces;
public interface IProductService : IDisposable
{
    Task Add(Product product);
    Task Update(Product product);
    Task Delete(Guid id);
}
