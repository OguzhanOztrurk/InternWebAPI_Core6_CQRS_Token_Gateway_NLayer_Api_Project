using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Intern.WorkHistory.Queries;

public class GetWorkHistoryQuery:IRequest<IResponse>
{
    public class GetWorkHistoryQueryHandler:IRequestHandler<GetWorkHistoryQuery,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IWorkHistoryRepository _workHistoryRepository;

        public GetWorkHistoryQueryHandler(ICurrentRepository currentRepository, IWorkHistoryRepository workHistoryRepository)
        {
            _currentRepository = currentRepository;
            _workHistoryRepository = workHistoryRepository;
        }

        public async Task<IResponse> Handle(GetWorkHistoryQuery request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());

            var workHistories = await _workHistoryRepository.GetListAsync(x =>
                x.InternId == _currentRepository.UserId() && x.DeleteDate == null);

            return new Response<IEnumerable<Entities.Concrete.WorkHistory>>(workHistories);
        }
    }
}