using System.Text.Json.Serialization;
using Entities.Base;

namespace Entities.Concrete;

public class AdvertCategory:IEntity
{
    #region Primary Key

    public int CategoryId { get; set; }

    #endregion

    #region Columns

    public string CategoryName { get; set; }
    public string? Categorydefinition { get; set; }

    #endregion

    #region Foreign Key

    [JsonIgnore]
    public ICollection<Advert> Adverts { get; set; }

    #endregion
}