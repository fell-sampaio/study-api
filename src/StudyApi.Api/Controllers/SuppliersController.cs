using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudyApi.Api.DTOs;
using StudyApi.Business.Interfaces;
using StudyApi.Business.Models;

namespace StudyApi.Api.Controllers;

[Route("api/suppliers")]
[ApiController]
public class SuppliersController(ISupplierRepository supplierRepository, ISupplierService supplierService, IMapper mapper) : MainController
{
    private readonly ISupplierRepository _supplierRepository = supplierRepository;
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
        var supplier = await GetSupplierProductsAdress(id);

        if (supplier == null) return NotFound();

        return Ok(supplier);
    }

    [HttpPost]
    public async Task<ActionResult<SupplierDto>> Add(SupplierDto supplierDto)
    {
        if (!ModelState.IsValid) return BadRequest();

        var supplier = _mapper.Map<Supplier>(supplierDto);
        var result = await _supplierService.Add(supplier);

        if (!result) return BadRequest();

        return Ok(supplier);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<SupplierDto>> Update(Guid id, SupplierDto supplierDto)
    {
        if (id != supplierDto.Id) return BadRequest();

        if (!ModelState.IsValid) return BadRequest();

        var supplier = _mapper.Map<Supplier>(supplierDto);
        var result = await _supplierService.Update(supplier);

        if (!result) return BadRequest();

        return Ok(supplier);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<SupplierDto>> Delete(Guid id)
    {
        var supplier = await GetSupplierWithAdress(id);

        if (supplier == null) return NotFound();

        var result = await _supplierService.Delete(id);

        if (!result) return BadRequest();

        return Ok(supplier);
    }

    public async Task<SupplierDto> GetSupplierProductsAdress(Guid id)
    {
        return _mapper.Map<SupplierDto>(await _supplierRepository.GetSupplierProductsAdress(id));
    }

    public async Task<SupplierDto> GetSupplierWithAdress(Guid id)
    {
        return _mapper.Map<SupplierDto>(await _supplierRepository.GetSupplierAdress(id));
    }
}
