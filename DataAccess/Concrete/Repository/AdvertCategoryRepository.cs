using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.Repository;

public class AdvertCategoryRepository:EfEntityRepositoryBase<AdvertCategory, AppDbContext>, IAdvertCategoryRepository
{
    public AdvertCategoryRepository(AppDbContext context) : base(context)
    {
    }
}