using Microsoft.EntityFrameworkCore;
using Service.Models;

namespace Service.Data;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveChanges()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }

    public async Task<Guid> Create(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }

        await _context.Customers.AddAsync(customer);
        
        return customer.Id;
    }

    public async Task Delete(Guid id)
    {
        var customer = await GetById(id);
        
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }

        _context.Customers.Remove(customer);
    }

    public async Task<ICollection<Customer>> GetAll()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer> GetById(Guid id)
    {
        return await _context.Customers.SingleOrDefaultAsync(x => x.Id == id);
    }
}