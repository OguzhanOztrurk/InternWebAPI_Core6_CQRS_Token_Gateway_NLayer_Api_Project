using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dto;

namespace DataAccess.Abstract;

public interface IAdvertCategoryRepository:IEntityRepository<AdvertCategory>
{
    
    Task<IEnumerable<CategoryInAdvertCountDTO>> GetCategoryList();
}