using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Repository;

public class WorkplaceRepository:EfEntityRepositoryBase<Workplace,AppDbContext>,IWorkplaceRepository
{
    public WorkplaceRepository(AppDbContext context) : base(context)
    {
    }

    public void WorkplaceControl(int workplaceId)
    {
        var result = Query().Any(x => x.WorkplaceId == workplaceId && x.DeleteDate==null);
        if (!result)
        {
            throw new System.Exception("Job not found");
        }
    }

    public async Task<IEnumerable<Workplace>> GetNotDeletedWorkplace(Guid userId)
    {
        var result = Context.Workplaces.Where(x => x.AdminId == userId && x.DeleteDate == null);
            
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

    public void AdminWordplaceControl(int wordplaceId, Guid userId)
    {
        var result = Query().Any(x => x.WorkplaceId == wordplaceId && x.AdminId == userId);
        if (!result)
        {
            throw new System.Exception("You are not authorized for this action");
        }
    }

    public async Task<IEnumerable<WorkplaceDTO>> GetWorkplaceAndAdminControl()
    {
        var result = await Query().Include(x => x.Admin)
            .Where(x => x.isActive == true &&
                        x.DeleteDate == null &&
                        x.Admin.User.isActive == true &&
                        x.Admin.User.DeleteDate == null)
            .Select(x => new WorkplaceDTO()
            {
                WorkplaceId = x.WorkplaceId,
                WorkplaceName = x.WorkplaceName,
                WorkplaceExplanation = x.WorkplaceExplanation,
                EmployeesCount = x.EmployeesCount,
                Vision = x.Vision,
                Mission = x.Mission
            }).ToListAsync();
        return result;
    }
}