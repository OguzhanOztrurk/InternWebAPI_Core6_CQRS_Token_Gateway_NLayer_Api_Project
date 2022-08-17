using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Intern.Appeal.Commands;

public class CreateAppealCommand:IRequest<IResponse>
{
    public int AdvertId { get; set; }
    
    public class CreateAppealCommandHandler:IRequestHandler<CreateAppealCommand,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAppealRepository _appealRepository;

        public CreateAppealCommandHandler(IAppealRepository appealRepository, ICurrentRepository currentRepository)
        {
            _appealRepository = appealRepository;
            _currentRepository = currentRepository;
            
        }

        public async Task<IResponse> Handle(CreateAppealCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());

            
            _appealRepository.AdvertAppealControl(request.AdvertId);
            _appealRepository.AppealControl(_currentRepository.UserId(),request.AdvertId);

            var appeal = new Entities.Concrete.Appeal();
            appeal.AdvertId = request.AdvertId;
            appeal.InternId = _currentRepository.UserId();
            appeal.EvaluationState = false;
            appeal.isActive = true;

            _appealRepository.Add(appeal);
            await _appealRepository.SaveChangesAsync();
            
            return new Response<Entities.Concrete.Appeal>(appeal);
        }
    }
}