using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dto;

namespace DataAccess.Abstract;

public interface IAdvertDetailRepository:IEntityRepository<AdvertDetail>
{
    Task<ActiveAdvertDetailListDTO> AdvertDetailList(int advertId);

}