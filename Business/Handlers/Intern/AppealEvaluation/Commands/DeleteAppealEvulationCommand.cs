using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Intern.AppealEvaluation.Commands;

public class DeleteAppealEvulationCommand:IRequest<IResponse>
{
    public int AppealId { get; set; }
    
    public class DeleteAppealEvulationCommandHandler:IRequestHandler<DeleteAppealEvulationCommand, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAppealEvaluationRepository _appealEvaluationRepository;
        private readonly IAppealRepository _appealRepository;

        public DeleteAppealEvulationCommandHandler(ICurrentRepository currentRepository, IAppealEvaluationRepository appealEvaluationRepository, IAppealRepository appealRepository)
        {
            _currentRepository = currentRepository;
            _appealEvaluationRepository = appealEvaluationRepository;
            _appealRepository = appealRepository;
        }

        public async Task<IResponse> Handle(DeleteAppealEvulationCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());
            _appealEvaluationRepository.AppealEvaluationInternControl(request.AppealId, _currentRepository.UserId());

            var appealEvaluation = await _appealEvaluationRepository.GetAsync(x => x.AppealId == request.AppealId);
            appealEvaluation.isActive = false;

            _appealEvaluationRepository.Update(appealEvaluation);
            await _appealEvaluationRepository.SaveChangesAsync();

            var appeal = await _appealRepository.GetAsync(x => x.AppealId == request.AppealId);
            appeal.DeleteDate = DateTime.Now;
            appeal.DeleteUserId = _currentRepository.UserId();

            _appealRepository.Update(appeal);
            await _appealRepository.SaveChangesAsync();

            return new Response<object>(null, "Has been deleted");

        }
    }
}