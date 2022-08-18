using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Intern.WorkHistory.Commands;

public class UpdateWorkHistoryStateCommand:IRequest<IResponse>
{
    public int WorkHistoryId { get; set; }
    
    public class UpdateWorkHistoryStateCommandHandler:IRequestHandler<UpdateWorkHistoryStateCommand,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IWorkHistoryRepository _workHistoryRepository;

        public UpdateWorkHistoryStateCommandHandler(ICurrentRepository currentRepository, IWorkHistoryRepository workHistoryRepository)
        {
            _currentRepository = currentRepository;
            _workHistoryRepository = workHistoryRepository;
        }

        public async Task<IResponse> Handle(UpdateWorkHistoryStateCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());
            _workHistoryRepository.WorkHistoryControl(request.WorkHistoryId,_currentRepository.UserId());

            var workHistory = await _workHistoryRepository.GetAsync(x => x.WorkHistoryId == request.WorkHistoryId);
            workHistory.isActive = !workHistory.isActive;

            _workHistoryRepository.Update(workHistory);
            await _workHistoryRepository.SaveChangesAsync();

            return new Response<Entities.Concrete.WorkHistory>(workHistory);
        }
    }
}