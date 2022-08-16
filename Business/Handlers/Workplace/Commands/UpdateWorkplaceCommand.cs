using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Workplace.Commands;

public class UpdateWorkplaceCommand:IRequest<IResponse>
{
    public int WorkplaceId { get; set; }
    public string WorkplaceName { get; set; }
    public string WorkplaceExplanation { get; set; }
    public int EmployeesCount { get; set; }
    public string Vision { get; set; }
    public string Mission { get; set; }
    
    public class UpdateWorkplaceCommandHandler:IRequestHandler<UpdateWorkplaceCommand, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IWorkplaceRepository _workplaceRepository;

        public UpdateWorkplaceCommandHandler(ICurrentRepository currentRepository, IWorkplaceRepository workplaceRepository)
        {
            _currentRepository = currentRepository;
            _workplaceRepository = workplaceRepository;
        }

        public async Task<IResponse> Handle(UpdateWorkplaceCommand request, CancellationToken cancellationToken)
        {
            _workplaceRepository.GetAdminControl(_currentRepository.UserId());
            _workplaceRepository.WorkplaceControl(request.WorkplaceId);
            _workplaceRepository.AdminWordplaceControl(request.WorkplaceId,_currentRepository.UserId());
            
            
            var workplace = _workplaceRepository.Get(x => x.WorkplaceId == request.WorkplaceId);

            workplace.WorkplaceName = request.WorkplaceName;
            workplace.WorkplaceExplanation = request.WorkplaceExplanation;
            workplace.EmployeesCount = request.EmployeesCount;
            workplace.Vision = request.Vision;
            workplace.Mission = request.Mission;


            _workplaceRepository.Update(workplace);
            await _workplaceRepository.SaveChangesAsync();
            return new Response<Entities.Concrete.Workplace>(workplace);
        }
    }
    
}