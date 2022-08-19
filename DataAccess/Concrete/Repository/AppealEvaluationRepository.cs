using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Concrete.Enum;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Repository;

public class AppealEvaluationRepository:EfEntityRepositoryBase<AppealEvaluation,AppDbContext>,IAppealEvaluationRepository
{
    public AppealEvaluationRepository(AppDbContext context) : base(context)
    {
    }

    public void AppealControl(int appealId)
    {
        var result = Context.Appeals.Any(x => x.AppealId == appealId && x.isActive == true && x.DeleteDate == null);
        if (!result)
        {
            throw new System.Exception("Relevant reference not found.");
        }
    }

    public async Task<IEnumerable<AppealEvaluation>> GetAppealEvaluation(Guid adminId)
    {
        
        var result = await (from appealEvaluation in Context.AppealEvaluations
                join appeal in Context.Appeals on appealEvaluation.AppealId equals appeal.AppealId
                join advert in Context.Adverts on appeal.AdvertId equals advert.AdvertId
                join workplace in Context.Workplaces on advert.WorkplaceId equals workplace.WorkplaceId
                join admin in Context.Admins on workplace.AdminId equals admin.UserId
                where
                      appeal.EvaluationStateEnum == EvaluationStateEnum.Approved &&
                      admin.UserId == adminId
                select new AppealEvaluation()
                {
                    AppealId = appeal.AppealId,
                    Conclusion = appealEvaluation.Conclusion,
                    ConclusionDetail = appealEvaluation.ConclusionDetail,
                    EvaluationStateEnum = appealEvaluation.EvaluationStateEnum,
                    isActive = appealEvaluation.isActive,
                }
            ).ToListAsync();
        return result;

    }

    public void AppealEvaluationAdminControl(int appealId, Guid adminId)
    {
        var result = Query().Include(x => x.Appeal)
            .ThenInclude(x => x.Advert)
            .ThenInclude(x => x.Workplace)
            .ThenInclude(x => x.Admin)
            .Any(x => (x.EvaluationStateEnum == EvaluationStateEnum.Approved ||
                       x.EvaluationStateEnum == EvaluationStateEnum.Denied) &&
                      x.AppealId == appealId &&
                      x.Appeal.Advert.Workplace.Admin.UserId == adminId);
        if (!result)
        {
            throw new System.Exception("Only assessments that have been completed can be deleted.");
        }
    }

    public async Task<IEnumerable<AppealEvaluation>> GetAppealEvaluationIntern(Guid internId)
    {
        var result = await (from appealEvaluation in Context.AppealEvaluations
                join appeal in Context.Appeals on appealEvaluation.AppealId equals appeal.AppealId
                where appeal.InternId == internId &&
                      appealEvaluation.AppealId == appeal.AppealId &&
                      appeal.DeleteDate ==null
                select new AppealEvaluation()
                {
                    AppealId = appealEvaluation.AppealId,
                    Conclusion = appealEvaluation.Conclusion,
                    ConclusionDetail = appealEvaluation.ConclusionDetail,
                    EvaluationStateEnum = appealEvaluation.EvaluationStateEnum,
                    isActive = appealEvaluation.isActive
                }
            ).ToListAsync();
        return result;
    }

    public void AppealEvaluationInternControl(int appealId, Guid internId)
    {
        var appealControl = Query().Include(x => x.Appeal)
            .Any(x => x.isActive == true && x.Appeal.DeleteDate == null);
        if (!appealControl)
        {
            throw new System.Exception("No Records Found");
        }




        var result = Query().Include(x => x.Appeal)
            .Any(x => x.AppealId == appealId &&
                        x.Appeal.InternId == internId &&
                        x.EvaluationStateEnum == EvaluationStateEnum.Cancel);
        if (!result)
        {
            throw new System.Exception("Only assessments that have been completed can be deleted.");
        }

    }
}