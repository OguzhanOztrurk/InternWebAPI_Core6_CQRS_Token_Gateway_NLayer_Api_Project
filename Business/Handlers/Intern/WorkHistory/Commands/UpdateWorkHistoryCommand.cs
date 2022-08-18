using Core.Wrappers;
using DataAccess.Abstract;
using DataAccess.Concrete.Enum;
using MediatR;

namespace Business.Handlers.Intern.WorkHistory.Commands;

public class UpdateWorkHistoryCommand:IRequest<IResponse>
{
    public int WorkHistoryId { get; set; }
    public string WorkplaceName { get; set; }
    public string OperationTime { get; set; }
    public WorkStateEnum WorkStateEnum { get; set; }
    
    public class UpdateWorkHistoryCommandHandler:IRequestHandler<UpdateWorkHistoryCommand,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IWorkHistoryRepository _workHistoryRepository;

        public UpdateWorkHistoryCommandHandler(ICurrentRepository currentRepository, IWorkHistoryRepository workHistoryRepository)
        {
            _currentRepository = currentRepository;
            _workHistoryRepository = workHistoryRepository;
        }

        public async Task<IResponse> Handle(UpdateWorkHistoryCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());
            _workHistoryRepository.WorkHistoryControl(request.WorkHistoryId,_currentRepository.UserId());

            var workHistory = await _workHistoryRepository.GetAsync(x => x.WorkHistoryId == request.WorkHistoryId);
            workHistory.WorkplaceName = request.WorkplaceName;
            workHistory.OperationTime = request.OperationTime;
            workHistory.WorkStateEnum = request.WorkStateEnum;

            _workHistoryRepository.Update(workHistory);
            await _workHistoryRepository.SaveChangesAsync();

            return new Response<Entities.Concrete.WorkHistory>(workHistory);
        }
    }
}