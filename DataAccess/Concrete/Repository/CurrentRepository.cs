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

    public string UserRole()
    {
        return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
    }
}