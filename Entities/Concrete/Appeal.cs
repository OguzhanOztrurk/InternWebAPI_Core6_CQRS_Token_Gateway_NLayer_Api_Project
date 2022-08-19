using System.Text.Json.Serialization;
using DataAccess.Concrete.Enum;
using Entities.Base;
using Entities.Base.GenericEntity;

namespace Entities.Concrete;

public class Appeal:IEntity,IPassive,IDeletion
{
    #region Primary Key

    public int AppealId { get; set; }

    #endregion

    #region Columns

    public int AdvertId { get; set; }
    public Guid InternId { get; set; }
    public EvaluationStateEnum EvaluationStateEnum { get; set; }

    #region Active

    public bool isActive { get; set; }

    #endregion

    #region Delete

    [JsonIgnore]
    public DateTime? DeleteDate { get; set; }
    [JsonIgnore]
    public Guid? DeleteUserId { get; set; }

    #endregion
    #endregion

    #region Foreign Key

    [JsonIgnore]
    public Advert Advert { get; set; }
    [JsonIgnore]
    public AppealEvaluation AppealEvaluation { get; set; }

    #endregion
    
}