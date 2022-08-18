using DataAccess.Concrete.Enum;

namespace Entities.Dto;

public class WorkHistoryDTO
{
    public string WorkplaceName { get; set; }
    public string OperationTime { get; set; }
    public WorkStateEnum WorkStateEnum { get; set; }
}