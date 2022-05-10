using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.Data;
using Service.Dtos;
using Service.Models;

namespace Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PromotionController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPromotionRepository _promotionRepository;
    private readonly IPackageRepository _packageRepository;

    public PromotionController(IPromotionRepository promotionRepository, IMapper mapper, IPackageRepository packageRepository)
    {
        _mapper = mapper;
        _packageRepository = packageRepository;
        _promotionRepository = promotionRepository;
    }
    
    [HttpGet]
    public async Task<ActionResult<ICollection<GetPromotionForViewDto>>> GetAll()
    {
        var promotions = await _promotionRepository.GetAll();

        return Ok(_mapper.Map<ICollection<GetPromotionForViewDto>>(promotions));
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Save(CreateUpdatePromotionDto input)
    {
        Promotion entity;
        if (input.Id == null || input.Id == Guid.Empty)
        {
            entity = await Create(input);
        }
        else
        {
            entity = await Update(input);
        }

        return entity.Id;
    }
    
    private async Task<Promotion> Update(CreateUpdatePromotionDto input)
    {
        await _promotionRepository.RemovePackageOfPromotion(input.Id);
        await _promotionRepository.SaveChanges();
        
        var entity = await _promotionRepository.GetById(input.Id);
        
        if (entity == null)
        {
            throw new KeyNotFoundException();
        }

        _mapper.Map<CreateUpdatePromotionDto, Promotion>(input, entity);
        await CalculateAmount(entity);

        await _promotionRepository.SaveChanges();
        
        return entity;
    }

    private async Task<Promotion> Create(CreateUpdatePromotionDto input)
    {
        var entity = _mapper.Map<CreateUpdatePromotionDto, Promotion>(input);

        await CalculateAmount(entity);

        await _promotionRepository.Create(entity);
        await _promotionRepository.SaveChanges();
        
        return entity;
    }

    private async Task CalculateAmount(Promotion entity)
    {
        var packageIds = entity.EmployeePackages.Select(x => x.PackageId).ToList();
        var packages = await _packageRepository.GetByIds(packageIds);

        entity.TotalAmount = 0M;
        foreach (var employeePackage in entity.EmployeePackages)
        {
            var package = packages.SingleOrDefault(x => x.Id == employeePackage.PackageId);
            if (package == null)
            {
                throw new KeyNotFoundException();
            }

            employeePackage.Amount = employeePackage.Quantity * package.Price;
            entity.TotalAmount += employeePackage.Amount;
        }
    }
}