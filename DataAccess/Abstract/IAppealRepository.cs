using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dto;

namespace DataAccess.Abstract;

public interface IAppealRepository:IEntityRepository<Appeal>
{
    void AdvertAppealControl(int advertId);
    void AppealControl(Guid internId,int advertId);
    void AppealAdminControl(int appealId, Guid userId);
    Task<IEnumerable<AppealAdminListDTO>> GetAdminAppealList(Guid userId);
    Guid GetAppealInternInfo(int appealId, Guid adminId);
}