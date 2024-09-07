using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudyApi.Api.DTOs;
using StudyApi.Business.Interfaces;
using StudyApi.Business.Models;

namespace StudyApi.Api.Controllers;

[Route("api/suppliers")]
[ApiController]
public class SuppliersController(
    ISupplierRepository supplierRepository,
    IAdressRepository adressRepository,
    ISupplierService supplierService,
    IMapper mapper,
    INotifier notifier) : MainController(notifier)
{
    private readonly ISupplierRepository _supplierRepository = supplierRepository;
    private readonly IAdressRepository _adressRepository = adressRepository;
    private readonly ISupplierService _supplierService = supplierService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Supplier>>> GetAll()
    {
        var suppliers = _mapper.Map<IEnumerable<SupplierDto>>(await _supplierRepository.GetAll());

        return Ok(suppliers);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<IEnumerable<Supplier>>> GetById(Guid id)
    {
        var supplier = await GetMappedSupplierProductsAdress(id);

        if (supplier == null) return NotFound();

        return Ok(supplier);
    }

    [HttpGet("get-adress/{id:guid}")]
    public async Task<AdressDto> GetAdressById(Guid id)
    {
        return _mapper.Map<AdressDto>(await _adressRepository.GetById(id));
    }

    [HttpPost]
    public async Task<ActionResult<SupplierDto>> Add(SupplierDto supplierDto)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _supplierService.Add(_mapper.Map<Supplier>(supplierDto));

        return CustomResponse(supplierDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<SupplierDto>> Update(Guid id, SupplierDto supplierDto)
    {
        if (id != supplierDto.Id) return BadRequest();

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _supplierService.Update(_mapper.Map<Supplier>(supplierDto));

        return CustomResponse(supplierDto);
    }

    [HttpPut("update-adress/{id:guid}")]
    public async Task<IActionResult> UpdateAdress(Guid id, AdressDto adressDto)
    {
        if (id != adressDto.Id) return BadRequest();

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _supplierService.UpdateAdress(_mapper.Map<Adress>(adressDto));

        return CustomResponse(adressDto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<SupplierDto>> Delete(Guid id)
    {
        var supplierDto = await GetMappedSupplierWithAdress(id);

        if (supplierDto == null) return NotFound();

        await _supplierService.Delete(id);

        return CustomResponse();
    }

    private async Task<SupplierDto> GetMappedSupplierProductsAdress(Guid id)
    {
        return _mapper.Map<SupplierDto>(await _supplierRepository.GetSupplierProductsAdress(id));
    }

    private async Task<SupplierDto> GetMappedSupplierWithAdress(Guid id)
    {
        return _mapper.Map<SupplierDto>(await _supplierRepository.GetSupplierAdress(id));
    }
}
