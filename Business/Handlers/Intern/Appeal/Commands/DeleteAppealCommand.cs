using Core.Wrappers;
using DataAccess.Abstract;
using DataAccess.Concrete.Enum;
using MediatR;

namespace Business.Handlers.Intern.Appeal.Commands;

public class DeleteAppealCommand:IRequest<IResponse>
{
    public int AppealId { get; set; }
    public class DeleteAppealCommandHandler:IRequestHandler<DeleteAppealCommand, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAppealRepository _appealRepository;

        public DeleteAppealCommandHandler(ICurrentRepository currentRepository, IAppealRepository appealRepository)
        {
            _currentRepository = currentRepository;
            _appealRepository = appealRepository;
        }

        public async Task<IResponse> Handle(DeleteAppealCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());
            _appealRepository.AppealAdminControl(request.AppealId,_currentRepository.UserId());

            var appeal = await _appealRepository.GetAsync(x => x.AppealId == request.AppealId);
            appeal.DeleteDate=DateTime.Now;
            appeal.DeleteUserId = _currentRepository.UserId();
            appeal.EvaluationStateEnum = EvaluationStateEnum.Cancel;

            _appealRepository.Update(appeal);
            await _appealRepository.SaveChangesAsync();
            return new Response<object>(null, "Appeal has been deleted");

        }
    }
}