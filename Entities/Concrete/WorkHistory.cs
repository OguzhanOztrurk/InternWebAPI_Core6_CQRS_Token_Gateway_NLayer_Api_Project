using DataAccess.Concrete.Enum;
using Entities.Base;
using Entities.Base.GenericEntity;

namespace Entities.Concrete;

public class WorkHistory:IEntity,IPassive,IDeletion
{
    #region Primary Key

    public int WorkHistoryId { get; set; }

    #endregion

    #region Columns

    public Guid InternId { get; set; }
    public string WorkplaceName { get; set; }
    public string OperationTime { get; set; }
    public WorkStateEnum WorkStateEnum { get; set; }

    #region Aktive

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