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

    public Guid GetAppealInternId(int appealId, Guid adminId)
    { 
        var result =   Query().Include(_ => _.Advert)
            .ThenInclude(_ => _.Workplace)
            .Where(_ => _.AppealId == appealId && _.Advert.Workplace.AdminId == adminId)
            .Select(_ => _.InternId).FirstAsync();

        return result.Result;

    }

    public async Task<IEnumerable<EducationDTO>> GetInternEducations(Guid userId)
    {
        var result = await Context.Educations.Where(x => x.InternId == userId && x.isActive == true && x.DeleteDate == null)
            .Select(x => new EducationDTO()
                {
                    SchoolName = x.SchoolName,
                    EducationLevelEnum = x.EducationLevelEnum,
                    EducationStateEnum = x.EducationStateEnum,
                    StartYear = x.StartYear,
                    EndYear = x.EndYear
                }
            ).ToListAsync();
        return result;
    }

    public async Task<IEnumerable<TalentDTO>> GetInternTalents(Guid userId)
    {
        var result =
            await Context.Talents.Where(x => x.InternId == userId && x.isActive == true && x.DeleteDate == null)
                .Select(x => new TalentDTO()
                {
                    TalentName = x.TalentName,
                    TalentExplanation = x.TalentExplanation,
                    TalentLevelEnum = x.TalentLevelEnum
                }).ToListAsync();
        return result;
    }

    public async Task<IEnumerable<WorkHistoryDTO>> GetInternWorkHistories(Guid userId)
    {
        var result = await Context.WorkHistories
            .Where(x => x.InternId == userId && x.isActive == true && x.DeleteDate == null)
            .Select(x => new WorkHistoryDTO()
            {
                WorkplaceName = x.WorkplaceName,
                OperationTime = x.OperationTime,
                WorkStateEnum = x.WorkStateEnum
            }).ToListAsync();
        return result;
    }

    public async Task<AppealInternInfoDTO> GetInternInfo(Guid adminId, int appealId)
    {
        var internId = GetAppealInternId(appealId, adminId);
        
        var intern = await Context.Users.Include(x => x.Intern)
            .Where(x => x.UserId == internId && x.DeleteDate == null).FirstAsync();
        var education = await GetInternEducations(internId);
        var talent = await GetInternTalents(internId);
        var workHistory = await GetInternWorkHistories(internId);

        AppealInternInfoDTO result = new AppealInternInfoDTO();
        result.InternId = intern.UserId;
        result.UserName = intern.UserName;
        result.FirstName = intern.FirstName;
        result.LastName = intern.LastName;
        result.Number = intern.Number;
        result.Email = intern.Email;
        result.Adress = intern.Intern.Adress;

        result.EducationDtos = education;
        result.TalentDtos = talent;
        result.WorkHistoryDtos = workHistory;

        return result;

    }
    
}