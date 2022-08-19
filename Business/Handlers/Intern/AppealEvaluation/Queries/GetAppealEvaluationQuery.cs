using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Intern.AppealEvaluation.Queries;

public class GetAppealEvaluationQuery:IRequest<IResponse>
{
    public class GetAppealEvaluationQueryHandler:IRequestHandler<GetAppealEvaluationQuery, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAppealEvaluationRepository _appealEvaluationRepository;

        public GetAppealEvaluationQueryHandler(ICurrentRepository currentRepository, IAppealEvaluationRepository appealEvaluationRepository)
        {
            _currentRepository = currentRepository;
            _appealEvaluationRepository = appealEvaluationRepository;
        }

        public async Task<IResponse> Handle(GetAppealEvaluationQuery request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());

            var appealEvaluation =
                await _appealEvaluationRepository.GetAppealEvaluationIntern(_currentRepository.UserId());

            return new Response<IEnumerable<Entities.Concrete.AppealEvaluation>>(appealEvaluation);
        }
    }
}