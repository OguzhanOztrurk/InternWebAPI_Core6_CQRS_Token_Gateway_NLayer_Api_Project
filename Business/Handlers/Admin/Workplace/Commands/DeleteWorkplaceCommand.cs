using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Workplace.Commands;

public class DeleteWorkplaceCommand:IRequest<IResponse>
{
    public int WorkplaceId { get; set; }

    public class DeleteWorkplaceCommandHandler:IRequestHandler<DeleteWorkplaceCommand,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IWorkplaceRepository _workplaceRepository;

        public DeleteWorkplaceCommandHandler(ICurrentRepository currentRepository, IWorkplaceRepository workplaceRepository)
        {
            _currentRepository = currentRepository;
            _workplaceRepository = workplaceRepository;
        }

        public async Task<IResponse> Handle(DeleteWorkplaceCommand request, CancellationToken cancellationToken)
        {
            _workplaceRepository.GetAdminControl(_currentRepository.UserId());
            _workplaceRepository.WorkplaceControl(request.WorkplaceId);
            _workplaceRepository.AdminWordplaceControl(request.WorkplaceId,_currentRepository.UserId());
            

            var workplace = _workplaceRepository.Get(x => x.WorkplaceId == request.WorkplaceId);
            workplace.DeleteDate=DateTime.Now;
            workplace.DeleteUserId = _currentRepository.UserId();

            _workplaceRepository.Update(workplace);
            await _workplaceRepository.SaveChangesAsync();
            return new Response<object>(null,"The workplace has been deleted.");
        }
    }
    
}