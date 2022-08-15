namespace Entities.Dto;

public class UserAdminInfoDTO
{
    #region Columns

    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Number { get; set; }
    public string? Email { get; set; }
    public string? Position { get; set; }

    #region Active

    public bool? isActive { get; set; }

    #endregion

    
    
    #endregion
}