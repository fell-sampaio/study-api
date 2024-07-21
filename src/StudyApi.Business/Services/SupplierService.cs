using StudyApi.Business.Interfaces;
using StudyApi.Business.Models;
using StudyApi.Business.Models.Validations;

namespace StudyApi.Business.Services;
public class SupplierService(ISupplierRepository supplierRepository,
                         IAdressRepository adressRepository,
                         INotifier notifier) : BaseService(notifier), ISupplierService
{
    private readonly ISupplierRepository _supplierRepository = supplierRepository;
    private readonly IAdressRepository _adressRepository = adressRepository;

    public async Task<bool> Add(Supplier supplier)
    {
        if (!ExecuteValidation(new SupplierValidation(), supplier)
            || !ExecuteValidation(new AdressValidation(), supplier.Adress)) return false;

        if (_supplierRepository.Search(f => f.Document == supplier.Document).Result.Any())
        {
            Notify("There is already a supplier with this document");
            return false;
        }

        await _supplierRepository.Add(supplier);
        return true;
    }

    public async Task<bool> Update(Supplier supplier)
    {
        if (!ExecuteValidation(new SupplierValidation(), supplier)) return false;

        if (_supplierRepository.Search(f => f.Document == supplier.Document && f.Id != supplier.Id).Result.Any())
        {
            Notify("There is already a supplier with this document");
            return false;
        }

        await _supplierRepository.Update(supplier);
        return true;
    }

    public async Task UpdateAdress(Adress adress)
    {
        if (!ExecuteValidation(new AdressValidation(), adress)) return;

        await _adressRepository.Update(adress);
    }

    public async Task<bool> Delete(Guid id)
    {
        if (_supplierRepository.GetSupplierProductsAdress(id).Result.Products.Any())
        {
            Notify("The supplier has registered products");
            return false;
        }

        var adress = await _adressRepository.GetAdressBySupplier(id);

        if (adress != null)
        {
            await _adressRepository.Delete(adress.Id);
        }

        await _supplierRepository.Delete(id);
        return true;
    }

    public void Dispose()
    {
        _supplierRepository?.Dispose();
        _adressRepository?.Dispose();
    }
}
