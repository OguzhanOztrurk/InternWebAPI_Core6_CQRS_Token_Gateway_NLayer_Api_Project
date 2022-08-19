using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Intern.Appeal.Commands;

public class UpdateAppealStateCommand:IRequest<IResponse>
{
    public int AppealId { get; set; }
    
    public class UpdateAppealStateCommandHandler:IRequestHandler<UpdateAppealStateCommand,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAppealRepository _appealRepository;

        public UpdateAppealStateCommandHandler(ICurrentRepository currentRepository, IAppealRepository appealRepository)
        {
            _currentRepository = currentRepository;
            _appealRepository = appealRepository;
        }

        public async Task<IResponse> Handle(UpdateAppealStateCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());
            _appealRepository.AppealStateControl(request.AppealId, _currentRepository.UserId());

            var appeal = await _appealRepository.GetAsync(x => x.AppealId == request.AppealId);
            appeal.isActive = !appeal.isActive;

            _appealRepository.Update(appeal);
            await _appealRepository.SaveChangesAsync();

            return new Response<Entities.Concrete.Appeal>(appeal);

        }
    }
}