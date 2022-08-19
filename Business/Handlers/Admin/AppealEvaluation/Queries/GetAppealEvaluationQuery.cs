using Core.Wrappers;
using DataAccess.Abstract;
using DataAccess.Concrete.Enum;
using MediatR;

namespace Business.Handlers.Admin.AppealEvaluation.Queries;

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
            _currentRepository.AdminControl(_currentRepository.UserId());

            var appealEvaluation =
                await _appealEvaluationRepository.GetAppealEvaluation( _currentRepository.UserId());

            return new Response<IEnumerable<Entities.Concrete.AppealEvaluation>>(appealEvaluation);
        }
    }
}