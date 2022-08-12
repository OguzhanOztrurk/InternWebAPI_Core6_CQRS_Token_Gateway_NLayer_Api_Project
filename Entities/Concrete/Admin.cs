using Entities.Base;

namespace Entities.Concrete;

public class Admin:IEntity
{
    #region PrimaryKey

    public Guid UserId { get; set; }

    #endregion

    #region Columns
    
    public string? Position { get; set; }

    #endregion

    #region Foreign Key

    public User User { get; set; }
    public Workplace Workplace { get; set; }

    #endregion
    
}