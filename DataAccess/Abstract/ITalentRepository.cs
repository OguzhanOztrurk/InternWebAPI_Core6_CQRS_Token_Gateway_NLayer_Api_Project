using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface ITalentRepository:IEntityRepository<Talent>
{
    void TalentControl(int talentId, Guid userId);
}