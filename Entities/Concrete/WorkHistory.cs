using System.Text.Json.Serialization;
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
    [JsonIgnore]
    public DateTime? DeleteDate { get; set;}
    [JsonIgnore]
    public Guid? DeleteUserId { get; set; }

    #endregion
    #endregion

    #region Foreign Key
    [JsonIgnore]
    public Intern Intern { get; set; }

    #endregion

    
    
}