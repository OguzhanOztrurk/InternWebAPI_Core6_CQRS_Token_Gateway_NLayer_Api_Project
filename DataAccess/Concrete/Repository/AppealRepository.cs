using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.Repository;

public class AppealRepository:EfEntityRepositoryBase<Appeal, AppDbContext>,IAppealRepository
{
    public AppealRepository(AppDbContext context) : base(context)
    {
    }
}