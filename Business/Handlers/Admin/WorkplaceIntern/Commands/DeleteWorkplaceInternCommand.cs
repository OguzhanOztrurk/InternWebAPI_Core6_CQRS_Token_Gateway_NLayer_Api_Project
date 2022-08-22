using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Admin.WorkplaceIntern.Commands;

public class DeleteWorkplaceInternCommand:IRequest<IResponse>
{
    public int  WorkplaceInterId { get; set; }
    
    public class DeleteWorkplaceInternCommandHandler:IRequestHandler<DeleteWorkplaceInternCommand, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IWorkplaceInternRepository _workplaceInternRepository;

        public DeleteWorkplaceInternCommandHandler(ICurrentRepository currentRepository, IWorkplaceInternRepository workplaceInternRepository)
        {
            _currentRepository = currentRepository;
            _workplaceInternRepository = workplaceInternRepository;
        }

        public async Task<IResponse> Handle(DeleteWorkplaceInternCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.AdminControl(_currentRepository.UserId());
            _workplaceInternRepository.WorkplaceInternStateControl(request.WorkplaceInterId,_currentRepository.UserId());

            var workplaceIntern = await 
                _workplaceInternRepository.GetAsync(x => x.WorkplaceInternId == request.WorkplaceInterId);
            workplaceIntern.DeleteDate=DateTime.Now;
            workplaceIntern.DeleteUserId = _currentRepository.UserId();

            return new Response<Entities.Concrete.WorkplaceIntern>(workplaceIntern);
        }
    }
}