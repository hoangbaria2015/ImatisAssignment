using Service.Models;

namespace Service.Data;

public interface IPackageRepository
{
    Task<bool> SaveChanges();
    Task<Guid> Create(Package package);
    Task Delete(Guid id);
    Task<ICollection<Package>> GetAll();
    Task<Package> GetById(Guid id);
    Task<ICollection<Package>> GetByIds(ICollection<Guid> ids);
}