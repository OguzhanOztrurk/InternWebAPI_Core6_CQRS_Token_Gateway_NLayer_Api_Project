using System.Text.Json.Serialization;
using Entities.Base;
using Entities.Base.GenericEntity;

namespace Entities.Concrete;

public class WorkplaceIntern:IEntity,IPassive,IDeletion
{
    #region Primary Key

    public int WorkplaceInternId { get; set; }

    #endregion

    #region Columns

    public int WorkplaceId { get; set; }
    public Guid InternId { get; set; }
    public DateTime AcceptDate { get; set; }
    
    [JsonIgnore] public int AdvertId { get; set; }

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

    #region ForeignKey

    [JsonIgnore]
    public Workplace Workplace { get; set; }

    #endregion

    
    
}