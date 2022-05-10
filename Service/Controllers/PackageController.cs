using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.Data;
using Service.Dtos;
using Service.Models;

namespace Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PackageController : ControllerBase
{
    private readonly IPackageRepository _packageRepository;
    private readonly IMapper _mapper;

    public PackageController(IPackageRepository packageRepository, IMapper mapper)
    {
        _packageRepository = packageRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<GetPackageForViewDto>>> GetAll()
    {
        var packages = await _packageRepository.GetAll();

        return Ok(_mapper.Map<ICollection<GetPackageForViewDto>>(packages));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ICollection<GetPackageForViewDto>>> GetById(Guid id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var package = await _packageRepository.GetById(id);
        
        if (package == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<GetPackageForViewDto>(package));
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Save(CreateUpdatePackageDto input)
    {
        Package entity;
        if (input.Id == null || $"{input.Id}" == "0")
        {
            entity = await Create(input);
        }
        else
        {
            entity = await Update(input);
        }

        return entity.Id;
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        await _packageRepository.Delete(id);
        await _packageRepository.SaveChanges();

        return Ok();
    }

    private async Task<Package> Update(CreateUpdatePackageDto input)
    {
        var entity = await _packageRepository.GetById(input.Id);
        
        if (entity == null)
        {
            throw new KeyNotFoundException();
        }

        _mapper.Map<CreateUpdatePackageDto, Package>(input, entity);
        await _packageRepository.SaveChanges();
        
        return entity;
    }

    private async Task<Package> Create(CreateUpdatePackageDto input)
    {
        var entity = _mapper.Map<CreateUpdatePackageDto, Package>(input);

        //Using Entity Framework inMem
        entity.Id = Guid.NewGuid();
        await _packageRepository.Create(entity);
        await _packageRepository.SaveChanges();
        
        return entity;
    }
}