using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Repository;

public class WorkplaceInternRepository:EfEntityRepositoryBase<WorkplaceIntern, AppDbContext>, IWorkplaceInternRepository
{
    public WorkplaceInternRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<WorkplaceIntern>> GetInternList(Guid adminId)
    {
        var result = await (from workplaceIntern in Context.WorkplaceInterns
            join user in Context.Users on workplaceIntern.InternId equals user.UserId
            join workplace in Context.Workplaces on workplaceIntern.WorkplaceId equals workplace.WorkplaceId
            where workplaceIntern.DeleteDate == null &&
                  workplaceIntern.isActive == true &&
                  user.isActive == true &&
                  user.DeleteDate == null &&
                  workplace.isActive == true &&
                  workplace.DeleteDate == null &&
                  workplace.AdminId == adminId
            select new WorkplaceIntern()
            {
                WorkplaceInternId = workplaceIntern.WorkplaceInternId,
                WorkplaceId = workplaceIntern.WorkplaceId,
                InternId = workplaceIntern.InternId,
                AcceptDate = workplaceIntern.AcceptDate,
                isActive = workplaceIntern.isActive
            }).ToListAsync();

        return result;
    }

    public void WorkplaceInternStateControl(int workplaceInternId, Guid adminId)
    {
        var result = Query().Include(x => x.Workplace)
            .Where(x => x.WorkplaceInternId == workplaceInternId && x.Workplace.AdminId == adminId &&
                        
                        x.DeleteDate == null &&
                        x.Workplace.isActive == true &&
                        x.Workplace.DeleteDate == null).Any();
        if (!result)
        {
            throw new System.Exception("Wrong information.");
        }
    }
}