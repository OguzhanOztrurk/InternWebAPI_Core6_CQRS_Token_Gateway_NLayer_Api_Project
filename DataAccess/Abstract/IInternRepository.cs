using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dto;

namespace DataAccess.Abstract;

public interface IInternRepository:IEntityRepository<Intern>
{
    Task<UserInternDTO> GetUserInternInfo(Guid userId);
}