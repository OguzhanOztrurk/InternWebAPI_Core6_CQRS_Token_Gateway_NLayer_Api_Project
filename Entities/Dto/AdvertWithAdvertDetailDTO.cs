namespace Entities.Dto;

public class AdvertWithAdvertDetailDTO
{
    public int AdvertId { get; set; }
    public int WorkplaceId { get; set; }
    public int CategoryId { get; set; }
    public string AdvertName { get; set; }
    public string AdvertSummary { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Quota { get; set; }
    
    public string CompanyInfo { get; set; }
    public string WorkDefinition { get; set; }
    public string Quality { get; set; }
    public string WorkEnvironment { get; set; }
    public string WorkHour { get; set; }
    public string Facilities { get; set; }
    public string Wage { get; set; }
    
    public bool isActive { get; set; }

    public DateTime CreatedDate { get; set; }
    public Guid CreatedUserId { get; set; }
    public DateTime? UpdateDate { get; set; }
    public Guid? UpdateUserId { get; set; }
    
}