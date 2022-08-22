using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.Enum;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IAppealEvaluationRepository:IEntityRepository<AppealEvaluation>
{
    void AppealControl(int appealId);
    Task<IEnumerable<AppealEvaluation>> GetAppealEvaluation(Guid adminId);
    void AppealEvaluationAdminControl(int appealId, Guid adminId);
    Task<IEnumerable<AppealEvaluation>> GetAppealEvaluationIntern(Guid internId);
    void AppealEvaluationInternControl(int appealId, Guid internId);
    void AppealEvaluationInternConfirmControl(int appealId, Guid userId);
    int GetAppealWorkplaceId(int appealId);
    int GetAdvertId(int appealId);
}