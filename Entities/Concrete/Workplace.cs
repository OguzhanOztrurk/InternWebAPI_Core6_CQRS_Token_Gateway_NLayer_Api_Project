using System.Text.Json.Serialization;
using Entities.Base;
using Entities.Base.GenericEntity;

namespace Entities.Concrete;

public class Workplace:IEntity,IPassive,IDeletion
{
    #region Primary Key

    public int WorkplaceId { get; set; }

    #endregion

    #region Columns

    public Guid AdminId { get; set; }
    public string WorkplaceName { get; set; }
    public string WorkplaceExplanation { get; set; }
    public int EmployeesCount { get; set; }
    public string Vision { get; set; }
    public string Mission { get; set; }

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
    public Admin Admin { get; set; }
    [JsonIgnore]
    public ICollection<Advert> Adverts { get; set; }
    [JsonIgnore]
    public ICollection<WorkplaceIntern> WorkplaceInterns { get; set; }

    #endregion
}