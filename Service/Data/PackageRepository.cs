using Microsoft.EntityFrameworkCore;
using Service.Models;

namespace Service.Data;

public class PackageRepository : IPackageRepository
{
    private readonly AppDbContext _context;

    public PackageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveChanges()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }

    public async Task<Guid> Create(Package package)
    {
        if (package == null)
        {
            throw new ArgumentNullException(nameof(package));
        }

        await _context.Packages.AddAsync(package);
        
        return package.Id;
    }

    public async Task Delete(Guid id)
    {
        var package = await GetById(id);
        
        if (package == null)
        {
            throw new ArgumentNullException(nameof(package));
        }

        _context.Packages.Remove(package);
    }

    public async Task<ICollection<Package>> GetAll()
    {
        return await _context.Packages.ToListAsync();
    }

    public async Task<Package> GetById(Guid id)
    {
        return await _context.Packages.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Package>> GetByIds(ICollection<Guid> ids)
    {
        return await _context.Packages.Where(x => ids.Contains(x.Id)).ToListAsync();
    }
}