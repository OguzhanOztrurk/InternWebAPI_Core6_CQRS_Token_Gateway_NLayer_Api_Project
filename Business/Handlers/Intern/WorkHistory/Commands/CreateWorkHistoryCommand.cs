using Core.Wrappers;
using DataAccess.Abstract;
using DataAccess.Concrete.Enum;
using MediatR;

namespace Business.Handlers.Intern.WorkHistory.Commands;

public class CreateWorkHistoryCommand:IRequest<IResponse>
{
    public string WorkplaceName { get; set; }
    public string OperationTime { get; set; }
    public WorkStateEnum WorkStateEnum { get; set; }
    
    public class CreateWorkHistoryCommandHandler:IRequestHandler<CreateWorkHistoryCommand,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IWorkHistoryRepository _workHistoryRepository;

        public CreateWorkHistoryCommandHandler(ICurrentRepository currentRepository, IWorkHistoryRepository workHistoryRepository)
        {
            _currentRepository = currentRepository;
            _workHistoryRepository = workHistoryRepository;
        }

        public async Task<IResponse> Handle(CreateWorkHistoryCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());

            var workHistory = new Entities.Concrete.WorkHistory();
            workHistory.InternId = _currentRepository.UserId();
            workHistory.WorkplaceName = request.WorkplaceName;
            workHistory.OperationTime = request.OperationTime;
            workHistory.WorkStateEnum = request.WorkStateEnum;
            workHistory.isActive = true;

            _workHistoryRepository.Add(workHistory);
            await _workHistoryRepository.SaveChangesAsync();

            return new Response<Entities.Concrete.WorkHistory>(workHistory);
            
        }
    }
}