using Service.Models;

namespace Service.Data;

public interface IPromotionRepository
{
    Task<bool> SaveChanges();
    Task<Guid> Create(Promotion promotion);
    Task Delete(Guid id);
    Task<ICollection<Promotion>> GetAll();
    Task<Promotion> GetById(Guid id);

    Task RemovePackageOfPromotion(Guid promotionId);
}