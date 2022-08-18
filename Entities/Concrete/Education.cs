using System.Text.Json.Serialization;
using DataAccess.Concrete.Enum;
using Entities.Base;
using Entities.Base.GenericEntity;

namespace Entities.Concrete;

public class Education:IEntity,IPassive,IDeletion
{
    #region Primary Key

    public int EducationId { get; set; }

    #endregion

    #region Columns

    public Guid InternId { get; set; }
    public string SchoolName { get; set; }
    public EducationLevelEnum EducationLevelEnum { get; set; }
    public string StartYear { get; set; }
    public string EndYear { get; set; }
    public EducationStateEnum EducationStateEnum { get; set; }

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
    public Intern Intern { get; set; }

    #endregion


    
    
}