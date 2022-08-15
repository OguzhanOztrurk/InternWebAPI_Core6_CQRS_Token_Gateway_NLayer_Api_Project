using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dto;

namespace DataAccess.Abstract;

public interface IAdminRepository:IEntityRepository<Admin>
{
    Task<UserAdminInfoDTO> GetUserAdminInfo(Guid adminId);
    void AdminControl(Guid adminId);
    
}