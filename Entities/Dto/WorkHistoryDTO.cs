using DataAccess.Concrete.Enum;

namespace Entities.Dto;

public class WorkHistoryDTO
{
    public int WorkHistoryId { get; set; }
    public Guid InternId { get; set; }
    public string WorkplaceName { get; set; }
    public string OperationTime { get; set; }
    public WorkStateEnum WorkStateEnum { get; set; }
}