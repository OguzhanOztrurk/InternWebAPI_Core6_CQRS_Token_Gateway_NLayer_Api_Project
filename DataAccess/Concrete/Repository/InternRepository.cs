using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Repository;

public class InternRepository:EfEntityRepositoryBase<Intern, AppDbContext>,IInternRepository
{
    public InternRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<UserInternDTO> GetUserInternInfo(Guid userId)
    {
        var result = await (from intern in Context.Interns
            join user in Context.Users on intern.UserId equals user.UserId
            where intern.UserId == userId
            select new UserInternDTO()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Number = user.Number,
                Email = user.Email,
                isActive = user.isActive,
                Adress = intern.Adress
            }).FirstAsync();
        return result;
    }
}