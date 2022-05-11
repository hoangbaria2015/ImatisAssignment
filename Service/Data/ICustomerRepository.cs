using Service.Models;

namespace Service.Data;

public interface ICustomerRepository
{
    Task<bool> SaveChanges();
    Task<Guid> Create(Customer package);
    Task Delete(Guid id);
    Task<ICollection<Customer>> GetAll();
    Task<Customer> GetById(Guid id);
}