using Core.Wrappers;
using DataAccess.Abstract;
using DataAccess.Concrete.Enum;
using MediatR;

namespace Business.Handlers.Admin.AppealEvaluation.Commands;

public class DeleteAppealEvaluationCommand:IRequest<IResponse>
{
    public int AppealId { get; set; }
    
    public class DeleteAppealEvaluationCommandHandler:IRequestHandler<DeleteAppealEvaluationCommand,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAppealRepository _appealRepository;
        private readonly IAppealEvaluationRepository _appealEvaluationRepository;

        public DeleteAppealEvaluationCommandHandler(ICurrentRepository currentRepository, IAppealRepository appealRepository, IAppealEvaluationRepository appealEvaluationRepository)
        {
            _currentRepository = currentRepository;
            _appealRepository = appealRepository;
            _appealEvaluationRepository = appealEvaluationRepository;
        }

        public async Task<IResponse> Handle(DeleteAppealEvaluationCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.AdminControl(_currentRepository.UserId());
            
            _appealEvaluationRepository.AppealEvaluationAdminControl(request.AppealId, _currentRepository.UserId());

            var appealEvaluation = await _appealEvaluationRepository.GetAsync(x => x.AppealId == request.AppealId);
            appealEvaluation.isActive = false;
            appealEvaluation.EvaluationStateEnum = EvaluationStateEnum.Cancel;

            _appealEvaluationRepository.Update(appealEvaluation);
            await _appealEvaluationRepository.SaveChangesAsync();

            var appeal = await _appealRepository.GetAsync(x => x.AppealId == request.AppealId);
            appeal.DeleteDate=DateTime.Now;
            appeal.DeleteUserId = _currentRepository.UserId();

            _appealRepository.Update(appeal);
            await _appealRepository.SaveChangesAsync();

            return new Response<object>(null, "Ad has been deleted");



        }
    }
}