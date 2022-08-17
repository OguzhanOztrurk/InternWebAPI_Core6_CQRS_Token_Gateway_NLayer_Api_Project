using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Repository;

public class AppealRepository:EfEntityRepositoryBase<Appeal, AppDbContext>,IAppealRepository
{
    public AppealRepository(AppDbContext context) : base(context)
    {
    }

    public void AdvertAppealControl(int advertId)
    {
        var advert =
             Context.Adverts.Where(x => x.AdvertId == advertId && x.DeleteDate == null && x.isActive == true&&
                                        x.StartDate <=DateTime.Now && x.EndDate>=DateTime.Now)
                .First();
        var workplace =  Context.Workplaces.Where(x =>
            x.WorkplaceId == advert.WorkplaceId && x.DeleteDate == null && x.isActive == true).First();
        var user =  Context.Users.Where(x =>
            x.UserId == workplace.AdminId && x.DeleteDate == null && x.isActive == true).First();
        if (user.FirstName==null)
        {
            throw new System.Exception("Ad not found.");
        }

    }

    public void AppealControl(Guid internId, int advertId)
    {
        var result = Query().Any(x => x.InternId == internId && x.AdvertId==advertId && x.DeleteDate==null);
        if (result)
        {
            throw new System.Exception("It is not possible to apply for the post again.");
        }
    }

    public void AppealAdminControl(int appealId, Guid userId)
    {
        var result = Query().Any(x => x.AppealId == appealId && x.InternId == userId && x.DeleteDate == null);
        if (!result)
        {
            throw new System.Exception("Appeal not found.");
        }
    }

    public async Task<IEnumerable<AppealAdminListDTO>> GetAdminAppealList(Guid userId)
    {
        var result = await (from workplace in Context.Workplaces
                join advert in Context.Adverts on workplace.WorkplaceId equals advert.WorkplaceId
                join appeal in Context.Appeals on advert.AdvertId equals appeal.AdvertId
                where workplace.AdminId == userId &&
                      appeal.isActive == true &&
                      appeal.DeleteDate == null
                select new AppealAdminListDTO()
                {
                    AppealId = appeal.AppealId,
                    AdvertId = appeal.AdvertId,
                    InternId = appeal.InternId
                }
            ).ToListAsync();
        return result;
    }

    public Guid GetAppealInternInfo(int appealId, Guid adminId)
    { 
        var result =   Query().Include(_ => _.Advert)
            .ThenInclude(_ => _.Workplace)
            .Where(_ => _.AppealId == appealId && _.Advert.Workplace.AdminId == adminId)
            .Select(_ => _.InternId).FirstAsync();

        return result.Result;

    }
}