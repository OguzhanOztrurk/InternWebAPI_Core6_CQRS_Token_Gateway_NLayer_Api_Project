using Core.Wrappers;
using DataAccess.Abstract;
using DataAccess.Concrete.Enum;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Admin.Appeal.Commands;

public class CreateAppealEvaluationCommand:IRequest<IResponse>
{
    public int AppealId { get; set; }
    public string Conclusion { get; set; }
    public string ConclusionDetail { get; set; }
    public bool ApprovalStatus { get; set; }

    public class CreateAppealEvaluationCommandHandler:IRequestHandler<CreateAppealEvaluationCommand,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAppealEvaluationRepository _appealEvaluationRepository;
        private readonly IAppealRepository _appealRepository;

        public CreateAppealEvaluationCommandHandler(ICurrentRepository currentRepository, IAppealEvaluationRepository appealEvaluationRepository, IAppealRepository appealRepository)
        {
            _currentRepository = currentRepository;
            _appealEvaluationRepository = appealEvaluationRepository;
            _appealRepository = appealRepository;
        }

        public async Task<IResponse> Handle(CreateAppealEvaluationCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.AdminControl(_currentRepository.UserId());
            _appealEvaluationRepository.AppealControl(request.AppealId);

            var appealStatusEnum = EvaluationStateEnum.Waiting;
            var evaluationStatusEnum =EvaluationStateEnum.Waiting;
            
            if (request.ApprovalStatus)
            {
                appealStatusEnum = EvaluationStateEnum.Approved;
                evaluationStatusEnum = EvaluationStateEnum.Waiting;
            }else if(!request.ApprovalStatus)
            {
                appealStatusEnum = EvaluationStateEnum.Denied;
                evaluationStatusEnum = EvaluationStateEnum.Cancel;
            }
            var appeal = await _appealRepository.GetAsync(x => x.AppealId == request.AppealId);
            appeal.EvaluationStateEnum = appealStatusEnum;
            appeal.isActive = false;

            _appealRepository.Update(appeal);
            await _appealRepository.SaveChangesAsync();
            
            
            var appealEvaluation = new Entities.Concrete.AppealEvaluation();
            appealEvaluation.AppealId = request.AppealId;
            appealEvaluation.Conclusion = request.Conclusion;
            appealEvaluation.ConclusionDetail = request.ConclusionDetail;
            appealEvaluation.EvaluationStateEnum = evaluationStatusEnum;
            appealEvaluation.isActive = true;

            _appealEvaluationRepository.Add(appealEvaluation);
            await _appealEvaluationRepository.SaveChangesAsync();
            
            return new Response<Entities.Concrete.AppealEvaluation>(appealEvaluation);
        }
    }
}