using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IEducationRepository:IEntityRepository<Education>
{
    void EducationControl(int educationId, Guid userId);
}