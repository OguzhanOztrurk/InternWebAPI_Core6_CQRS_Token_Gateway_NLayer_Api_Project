using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.Repository;

public class AdvertDetailRepository:EfEntityRepositoryBase<AdvertDetail, AppDbContext>,IAdvertDetailRepository
{
    public AdvertDetailRepository(AppDbContext context) : base(context)
    {
    }
}