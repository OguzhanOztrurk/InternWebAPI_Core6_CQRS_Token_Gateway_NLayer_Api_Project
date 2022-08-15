
using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Workplace.Commands;

public class StateWorkplaceCommand:IRequest<IResponse>
{
    public int WorkplaceId { get; set; }
    public class StateWorkplaceCommandHandler:IRequestHandler<StateWorkplaceCommand, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IWorkplaceRepository _workplaceRepository;

        public StateWorkplaceCommandHandler(ICurrentRepository currentRepository, IWorkplaceRepository workplaceRepository)
        {
            _currentRepository = currentRepository;
            _workplaceRepository = workplaceRepository;
        }

        public async Task<IResponse> Handle(StateWorkplaceCommand request, CancellationToken cancellationToken)
        {
            _workplaceRepository.GetAdminControl(_currentRepository.UserId());
           

            var workplace = _workplaceRepository.Get(x => x.WorkplaceId == request.WorkplaceId);
            workplace.isActive =  !workplace.isActive;

            _workplaceRepository.Update(workplace);
            await _workplaceRepository.SaveChangesAsync();
            return new Response<Entities.Concrete.Workplace>(workplace);
        }
    }
}