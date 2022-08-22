using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IWorkplaceInternRepository:IEntityRepository<WorkplaceIntern>
{
    Task<IEnumerable<WorkplaceIntern>> GetInternList(Guid adminId);
    void WorkplaceInternStateControl(int workplaceInternId, Guid adminId);
}