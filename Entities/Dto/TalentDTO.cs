using DataAccess.Concrete.Enum;

namespace Entities.Dto;

public class TalentDTO
{
    public string TalentName { get; set; }
    public string TalentExplanation { get; set; }
    public TalentLevelEnum TalentLevelEnum { get; set; }
}