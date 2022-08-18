using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IWorkHistoryRepository:IEntityRepository<WorkHistory>
{
    void WorkHistoryControl(int workHistoryId, Guid userId);
}