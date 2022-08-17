using System.Security.Claims;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace DataAccess.Concrete.Repository;

public class CurrentRepository:EfEntityRepositoryBase<User,AppDbContext>,ICurrentRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public CurrentRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId()
    {
        var userId= _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Guid.Parse(userId);
    }

    public void AdminControl(Guid userId)
    {
        var result = Context.Admins.Where(x => x.UserId == userId).Any();
                if (!result)
                {
                    throw new System.Exception("You are not authorized to use this field.");
                }
    }

    public void UserControl(Guid userId)
    {
        var result = Context.Interns.Where(x => x.UserId == userId).Any();
        if (!result)
        {
            throw new System.Exception("You are not authorized to use this field.");
        }
    }
}