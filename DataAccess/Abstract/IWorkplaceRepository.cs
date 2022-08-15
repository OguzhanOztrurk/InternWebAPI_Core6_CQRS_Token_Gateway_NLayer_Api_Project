using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IWorkplaceRepository:IEntityRepository<Workplace>
{
    void WorkplaceControl(Guid userId);
    Task<IEnumerable<Workplace>> GetNotDeletedWorkplace(Guid userId);
    void GetAdminControl(Guid userId);

}