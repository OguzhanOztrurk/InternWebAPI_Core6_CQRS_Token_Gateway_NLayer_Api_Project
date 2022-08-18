using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.Repository;

public class TalentRepository:EfEntityRepositoryBase<Talent,AppDbContext>,ITalentRepository
{
    public TalentRepository(AppDbContext context) : base(context)
    {
    }

    public void TalentControl(int talentId, Guid userId)
    {
        var result = Query().Any(x => x.TalentId == talentId && x.InternId == userId && x.DeleteDate == null);
        if (!result)
        {
            throw new System.Exception("No Records Found");
        }
    }
}