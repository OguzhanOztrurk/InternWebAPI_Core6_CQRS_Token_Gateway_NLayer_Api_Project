using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.Repository;

public class EducationRepository:EfEntityRepositoryBase<Education,AppDbContext>,IEducationRepository
{
    public EducationRepository(AppDbContext context) : base(context)
    {
    }

    public void EducationControl(int educationId, Guid userId)
    {
        var result = Query().Any(x => x.EducationId == educationId && x.DeleteDate == null && x.InternId == userId);
        if (!result)
        {
            throw new System.Exception("No Records Found");
        }
    }
}