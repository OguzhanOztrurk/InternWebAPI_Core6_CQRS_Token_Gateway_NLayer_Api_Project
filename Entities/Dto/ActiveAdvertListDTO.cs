namespace Entities.Dto;

public class ActiveAdvertListDTO
{
    #region Primary Key

    public int AdvertId { get; set; }

    #endregion

    #region Columns

    public int WorkplaceId { get; set; }
    public int CategoryId { get; set; }
    public string WorkplaceName { get; set; }
    public string CategoryName { get; set; }
    public string AdvertName { get; set; }
    public string AdvertSummary { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Quota { get; set; }
    
    #region Create

    public DateTime CreatedDate { get; set; }
    

    #endregion
    
    #endregion
}