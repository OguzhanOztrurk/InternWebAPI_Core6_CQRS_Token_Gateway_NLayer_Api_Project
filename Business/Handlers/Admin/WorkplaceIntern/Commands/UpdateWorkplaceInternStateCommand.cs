using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Admin.WorkplaceIntern.Commands;

public class UpdateWorkplaceInternStateCommand:IRequest<IResponse>
{
    public int workplaceInternId { get; set; }
    public bool WorkplaceInternState { get; set; }
    
    public class UpdateWorkplaceInternStateCommandHandler:IRequestHandler<UpdateWorkplaceInternStateCommand, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IWorkplaceInternRepository _workplaceInternRepository;

        public UpdateWorkplaceInternStateCommandHandler(ICurrentRepository currentRepository, IWorkplaceInternRepository workplaceInternRepository)
        {
            _currentRepository = currentRepository;
            _workplaceInternRepository = workplaceInternRepository;
        }

        public async Task<IResponse> Handle(UpdateWorkplaceInternStateCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.AdminControl(_currentRepository.UserId());
            _workplaceInternRepository.WorkplaceInternStateControl(request.workplaceInternId,_currentRepository.UserId());

            var workplaceIntern =
                await _workplaceInternRepository.GetAsync(x => x.WorkplaceInternId == request.workplaceInternId);
            workplaceIntern.isActive = !workplaceIntern.isActive;

            _workplaceInternRepository.Update(workplaceIntern);
            await _workplaceInternRepository.SaveChangesAsync();

            return new Response<Entities.Concrete.WorkplaceIntern>(workplaceIntern);

        }
    }
}