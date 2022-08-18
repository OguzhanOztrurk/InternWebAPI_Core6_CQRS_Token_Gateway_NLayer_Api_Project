using DataAccess.Concrete.Enum;

namespace Entities.Dto;

public class EducationDTO
{
    
    public string SchoolName { get; set; }
    public EducationLevelEnum EducationLevelEnum { get; set; }
    public string StartYear { get; set; }
    public string EndYear { get; set; }
    public EducationStateEnum EducationStateEnum { get; set; }
    
}