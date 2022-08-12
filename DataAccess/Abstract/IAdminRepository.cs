using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IAdminRepository:IEntityRepository<Admin>
{
    
}