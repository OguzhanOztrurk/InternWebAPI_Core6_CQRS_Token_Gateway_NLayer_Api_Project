using Core.Wrappers;
using DataAccess.Abstract;
using DataAccess.Concrete.Enum;
using MediatR;

namespace Business.Handlers.Intern.Appeal.Queries;

public class GetAppealQuery:IRequest<IResponse>
{
    public class GetAppealQueryHandler:IRequestHandler<GetAppealQuery, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAppealRepository _appealRepository;

        public GetAppealQueryHandler(IAppealRepository appealRepository, ICurrentRepository currentRepository)
        {
            _appealRepository = appealRepository;
            _currentRepository = currentRepository;
        }

        public async Task<IResponse> Handle(GetAppealQuery request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());
            var appeals = await _appealRepository.GetListAsync(x => x.InternId == _currentRepository.UserId() && x.EvaluationStateEnum != EvaluationStateEnum.Cancel && x.EvaluationStateEnum!=EvaluationStateEnum.Denied && x.DeleteDate==null);

            return new Response<IEnumerable<Entities.Concrete.Appeal>>(appeals);
        }
    }
}