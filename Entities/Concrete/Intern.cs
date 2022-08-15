using Entities.Base;

namespace Entities.Concrete;

public class Intern:IEntity
{
    #region PrimaryKey

    public Guid UserId { get; set; }

    #endregion

    #region Columns

    public string? Adress { get; set; }
    
    
    #endregion

    #region ForeignKey

    public User User { get; set; }
    public ICollection<Education> Education { get; set; }
    public ICollection<WorkHistory>  WorkHistory { get; set; }
    public ICollection<Talent> Talent { get; set; }
    public WorkplaceIntern WorkplaceIntern { get; set; }

    #endregion
}