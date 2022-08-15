using System.Text.Json.Serialization;
using Entities.Base;
using Entities.Base.GenericEntity;

namespace Entities.Concrete;

public class Advert:IEntity,IPassive,IDeletion,ICreatable
{
    #region Primary Key

    public int AdvertId { get; set; }

    #endregion

    #region Columns

    public int WorkplaceId { get; set; }
    public int CategoryId { get; set; }
    public string AdvertName { get; set; }
    public string AdvertSummary { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Quota { get; set; }

    #region Active

    public bool isActive { get; set; }

    #endregion

    #region Delete

    [JsonIgnore]
    public DateTime? DeleteDate { get; set; }
    [JsonIgnore]
    public Guid? DeleteUserId { get; set; }

    #endregion

    #region Create

    public DateTime CreatedDate { get; set; }
    public Guid CreatedUserId { get; set; }
    public DateTime? UpdateDate { get; set; }
    public Guid? UpdateUserId { get; set; }

    #endregion
    
    #endregion

    #region Foreign Key

    [JsonIgnore]
    public AdvertCategory AdvertCategory { get; set; }
    [JsonIgnore]
    public AdvertDetail AdvertDetail { get; set; }
    [JsonIgnore]
    public ICollection<Appeal> Appeals { get; set; }
    [JsonIgnore]
    public Workplace Workplace { get; set; }

    #endregion
    
    
    
}