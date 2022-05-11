using Microsoft.EntityFrameworkCore;
using Service.Models;

namespace Service.Data;

public class PromotionRepository : IPromotionRepository
{
    private readonly AppDbContext _context;

    public PromotionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveChanges()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }

    public async Task<Guid> Create(Promotion promotion)
    {
        if (promotion == null)
        {
            throw new ArgumentNullException(nameof(promotion));
        }

        await _context.Promotions.AddAsync(promotion);
        
        return promotion.Id;
    }

    public async Task Delete(Guid id)
    {
        var promotion = await GetById(id);
        
        if (promotion == null)
        {
            throw new ArgumentNullException(nameof(promotion));
        }

        _context.Promotions.Remove(promotion);
    }

    public async Task<ICollection<Promotion>> GetAll()
    {
        return await _context.Promotions
            .Include(x => x.Customer)
            .Include(x => x.EmployeePackages)
            .ThenInclude(x => x.Package)
            .ToListAsync();
    }
    
    public async Task RemovePackageOfPromotion(Guid promotionId)
    {
        var packages = await _context.EmployeePackages.Where(x => x.PromotionId == promotionId).ToListAsync();
        
        _context.EmployeePackages.RemoveRange(packages);
    }

    public async Task<Promotion> GetById(Guid id)
    {
        return await _context.Promotions
            .Include(x => x.Customer)
            .Include(x => x.EmployeePackages)
            .SingleOrDefaultAsync(x => x.Id == id);
    }
}