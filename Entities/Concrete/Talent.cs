using DataAccess.Concrete.Enum;
using Entities.Base;
using Entities.Base.GenericEntity;

namespace Entities.Concrete;

public class Talent:IEntity,IPassive,IDeletion
{
    #region Primary Key

    public int TalentId { get; set; }

    #endregion

    #region Columns

    public Guid InternId { get; set; }
    public string TalentName { get; set; }
    public string TalentExplanation { get; set; }
    public TalentLevelEnum TalentLevelEnum { get; set; }

    #region Active

    public bool isActive { get; set; }

    #endregion

    #region Delete
    
    public DateTime? DeleteDate { get; set; }
    public Guid? DeleteUserId { get; set; }

    #endregion

    #endregion

    #region Foreign Key

    public Intern Intern { get; set; }

    #endregion

    
    
}