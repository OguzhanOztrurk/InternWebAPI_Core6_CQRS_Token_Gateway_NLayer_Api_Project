namespace Entities.Base.GenericEntity;

public interface ICreatable
{
    public DateTime CreatedDate { get; set; }
    public Guid CreatedUserId { get; set; }
    public DateTime? UpdateDate { get; set; }
    public Guid? UpdateUserId { get; set; }
    
}