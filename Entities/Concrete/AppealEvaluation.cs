using System.Text.Json.Serialization;
using Entities.Base;
using Entities.Base.GenericEntity;

namespace Entities.Concrete;

public class AppealEvaluation:IEntity,IPassive
{
    #region Primary Key

    public int AppealId { get; set; }

    #endregion

    #region Columns

    public string Conclusion { get; set; }
    public string ConclusionDetail { get; set; }
    public bool InternApproval { get; set; }

    #region Active

    public bool isActive { get; set; }

    #endregion

    #endregion

    #region Foreign Key

    [JsonIgnore]
    public Appeal Appeal { get; set; }

    #endregion

    
    
}