using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Intern.WorkHistory.Commands;

public class DeleteWorkHistoryCommand:IRequest<IResponse>
{
    public int WorkHistoryId { get; set; }
    
    public class DeleteWorkHistoryCommandHandler:IRequestHandler<DeleteWorkHistoryCommand,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IWorkHistoryRepository _workHistoryRepository;

        public DeleteWorkHistoryCommandHandler(ICurrentRepository currentRepository, IWorkHistoryRepository workHistoryRepository)
        {
            _currentRepository = currentRepository;
            _workHistoryRepository = workHistoryRepository;
        }

        public async Task<IResponse> Handle(DeleteWorkHistoryCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());
            _workHistoryRepository.WorkHistoryControl(request.WorkHistoryId,_currentRepository.UserId());

            var workHistory = await _workHistoryRepository.GetAsync(x => x.WorkHistoryId == request.WorkHistoryId);
            workHistory.DeleteDate=DateTime.Now;
            workHistory.DeleteUserId = _currentRepository.UserId();

            _workHistoryRepository.Update(workHistory);
            await _workHistoryRepository.SaveChangesAsync();

            return new Response<object>(null,"Has been deleted");
        }
    }
}