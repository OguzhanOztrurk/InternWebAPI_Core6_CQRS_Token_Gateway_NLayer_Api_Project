using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Repository;

public class AdminRepository:EfEntityRepositoryBase<Admin,AppDbContext>,IAdminRepository
{
    public AdminRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<UserAdminInfoDTO> GetUserAdminInfo(Guid adminId)
    {
        var result = await (from admin in Context.Admins
            join user in Context.Users on admin.UserId equals user.UserId
            where admin.UserId == adminId
            select new UserAdminInfoDTO()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Number = user.Number,
                Email = user.Email,
                isActive = user.isActive,
                Position = admin.Position
            }).FirstAsync();
        return result;
    }

    public void AdminControl(Guid adminId)
    {
        var result = Context.Admins.Where(x => x.UserId == adminId).Any();
        if (!result)
        {
            throw new System.Exception("You are not authorized to use this field.");
        }
    }
}