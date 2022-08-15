using System.Text.Json.Serialization;
using Entities.Base;

namespace Entities.Concrete;

public class AdvertDetail:IEntity
{
    #region Primary Key

    public int AdvertId { get; set; }
    
    #endregion

    #region Columns

    public string CompanyInfo { get; set; }
    public string WorkDefinition { get; set; }
    public string Quality { get; set; }
    public string WorkEnvironment { get; set; }
    public string WorkHour { get; set; }
    public string Facilities { get; set; }
    public string Wage { get; set; }

    #endregion

    #region Foreign Key

    [JsonIgnore]
    public Advert Advert { get; set; }

    #endregion
}