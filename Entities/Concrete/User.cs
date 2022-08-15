using System.Text.Json.Serialization;
using Entities.Base;
using Entities.Base.GenericEntity;

namespace Entities.Concrete;

public class User:IEntity,IPassive,IDeletion
{
    #region Primary Key

    public Guid UserId { get; set; }

    #endregion

    #region Columns

    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [JsonIgnore]
    public string? Number { get; set; }
    [JsonIgnore]
    public string? Email { get; set; }
    [JsonIgnore] public byte[] PasswordHash { get; set; }
    [JsonIgnore] public byte[] PasswordSalt { get; set; }

    #region Active
    [JsonIgnore]
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
    public Intern Intern { get; set; }

    #endregion

    
}