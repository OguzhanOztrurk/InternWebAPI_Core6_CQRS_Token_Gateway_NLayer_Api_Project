using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;
using Entities.Concrete;
namespace Business.Handlers.Workplace.Commands;

public class CreateWorkplaceCommand:IRequest<IResponse>
{
    public string WorkplaceName { get; set; }
    public string WorkplaceExplanation { get; set; }
    public int EmployeesCount { get; set; }
    public string Vision { get; set; }
    public string Mission { get; set; }
    
    
    public class CreateWorkplaceCommandHandler:IRequestHandler<CreateWorkplaceCommand, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IWorkplaceRepository _workplaceRepository;

        public CreateWorkplaceCommandHandler(ICurrentRepository currentRepository, IWorkplaceRepository workplaceRepository)
        {
            _currentRepository = currentRepository;
            _workplaceRepository = workplaceRepository;
        }

        public async Task<IResponse> Handle(CreateWorkplaceCommand request, CancellationToken cancellationToken)
        {
            _workplaceRepository.GetAdminControl(_currentRepository.UserId());
            
            var workplace = new Entities.Concrete.Workplace();
            workplace.AdminId = _currentRepository.UserId();
            workplace.WorkplaceName = request.WorkplaceName;
            workplace.WorkplaceExplanation = request.WorkplaceExplanation;
            workplace.EmployeesCount = request.EmployeesCount;
            workplace.Vision = request.Vision;
            workplace.Mission = request.Mission;
            workplace.isActive = true;

            _workplaceRepository.Add(workplace);
            await _workplaceRepository.SaveChangesAsync();
            return new Response<Entities.Concrete.Workplace>(workplace);
        }
    }
}