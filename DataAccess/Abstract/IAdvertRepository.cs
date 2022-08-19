using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dto;

namespace DataAccess.Abstract;

public interface IAdvertRepository:IEntityRepository<Advert>
{
    Task<IEnumerable<AdvertWithAdvertDetailDTO>> GetAdvertList(int workplaceId);
    void AdvertWorkplaceControl(int workplaceId);
    void WorkplaceControl(int WorkplaceId);
    void AdminWorkplaceControl(int workplaceId, Guid userId);
    void WorkplaceAdvertControl(int advertId, Guid userId);
    void AdvertControl(int advertId);
    void AdvertWorkplaceActive(int advertId);

    void AdvertStartDateControl(DateTime startDate, DateTime endDate);

    Task<AdvertWithAdvertDetailDTO> GetAdvert(int advertId);
    Task<IEnumerable<ActiveAdvertListDTO>> GetAdvertList();
    Task<IEnumerable<ActiveAdvertListDTO>> GetAdvertInCategoryList(int categoryId);
    Task<IEnumerable<ActiveAdvertListDTO>> GetAdvertInWorkplaceList(int workplaceId);
}