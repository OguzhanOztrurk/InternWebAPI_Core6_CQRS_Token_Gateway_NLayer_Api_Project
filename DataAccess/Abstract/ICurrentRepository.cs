using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface ICurrentRepository:IEntityRepository<User>
{
    Guid UserId();
    string UserRole();
}