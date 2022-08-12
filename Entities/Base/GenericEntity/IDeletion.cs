namespace Entities.Base.GenericEntity;

public interface IDeletion
{
    #region Delete

   
    public DateTime? DeleteDate { get; set; }
    public Guid? DeleteUserId { get; set; }

    #endregion
}