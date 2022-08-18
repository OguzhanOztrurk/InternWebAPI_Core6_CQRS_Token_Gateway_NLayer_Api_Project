namespace Entities.Dto;

public class AppealInternInfoDTO
{
    public Guid InternId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Number { get; set; }
    public string? Email { get; set; }
    public string? Adress { get; set; }

    public IEnumerable<EducationDTO> EducationDtos { get; set; }
    public IEnumerable<TalentDTO> TalentDtos { get; set; }
    public IEnumerable<WorkHistoryDTO> WorkHistoryDtos { get; set; }
}