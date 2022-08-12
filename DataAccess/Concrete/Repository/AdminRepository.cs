using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.Repository;

public class AdminRepository:EfEntityRepositoryBase<Admin,AppDbContext>,IAdminRepository
{
    public AdminRepository(AppDbContext context) : base(context)
    {
    }
}