using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.Data;
using Service.Dtos;
using Service.Models;

namespace Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<GetCustomerForViewDto>>> GetAll()
    {
        var customers = await _customerRepository.GetAll();

        return Ok(_mapper.Map<ICollection<GetCustomerForViewDto>>(customers));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ICollection<GetCustomerForViewDto>>> GetById(Guid id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var customer = await _customerRepository.GetById(id);
        
        if (customer == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<GetCustomerForViewDto>(customer));
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Save(CreateUpdateCustomerDto input)
    {
        Customer entity;
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
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        await _customerRepository.Delete(id);
        await _customerRepository.SaveChanges();

        return Ok();
    }

    private async Task<Customer> Update(CreateUpdateCustomerDto input)
    {
        var entity = await _customerRepository.GetById(input.Id);
        
        if (entity == null)
        {
            throw new KeyNotFoundException();
        }

        _mapper.Map<CreateUpdateCustomerDto, Customer>(input, entity);
        await _customerRepository.SaveChanges();
        
        return entity;
    }

    private async Task<Customer> Create(CreateUpdateCustomerDto input)
    {
        var entity = _mapper.Map<CreateUpdateCustomerDto, Customer>(input);

        //Using Entity Framework inMem
        entity.Id = Guid.NewGuid();
        await _customerRepository.Create(entity);
        await _customerRepository.SaveChanges();
        
        return entity;
    }
}