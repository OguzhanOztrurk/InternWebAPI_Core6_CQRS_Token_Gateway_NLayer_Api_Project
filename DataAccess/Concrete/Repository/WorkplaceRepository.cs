using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Repository;

public class WorkplaceRepository:EfEntityRepositoryBase<Workplace,AppDbContext>,IWorkplaceRepository
{
    public WorkplaceRepository(AppDbContext context) : base(context)
    {
    }

    public void WorkplaceControl(Guid userId)
    {
        var result = Query().Any(x=>x.AdminId==userId && x.DeleteDate == null);
        if (result)
        {
            throw new System.Exception("To add a new workplace, you must delete your current workplace.");
        }
    }

    public async Task<IEnumerable<Workplace>> GetNotDeletedWorkplace(Guid userId)
    {
        var result = Context.Workplaces.Where(x => x.AdminId == userId)
            .Where(x => x.DeleteDate == null).ToList();
        return result;
    }

    public void GetAdminControl(Guid userId)
    {
        var result = Context.Admins.Where(x => x.UserId == userId).Any();
        if (!result)
        {
            throw new System.Exception("You are not authorized to use this field");
        }
    }
}