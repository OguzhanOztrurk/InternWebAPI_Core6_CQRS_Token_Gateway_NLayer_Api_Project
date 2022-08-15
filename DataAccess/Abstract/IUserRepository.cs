using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IUserRepository:IEntityRepository<User>
{
    void UserNameControl(string userName);
    void UserDeleteControl(Guid userId);
}