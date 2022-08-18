using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.Repository;

public class WorkHistoryRepository:EfEntityRepositoryBase<WorkHistory, AppDbContext>,IWorkHistoryRepository
{
    public WorkHistoryRepository(AppDbContext context) : base(context)
    {
    }

    public void WorkHistoryControl(int workHistoryId, Guid userId)
    {
        var result = Query().Any(x => x.WorkHistoryId == workHistoryId && x.InternId == userId && x.DeleteDate == null);
        if (!result)
        {
            throw new System.Exception("No Records Found");
        }
    }
}