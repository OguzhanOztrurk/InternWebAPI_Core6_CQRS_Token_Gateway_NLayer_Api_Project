using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Admin.Appeal.Query;

public class GetAppealInternInfoQuery:IRequest<IResponse>
{
    public int AppealId { get; set; }
    public class GetAppealInternInfoQueryHandler:IRequestHandler<GetAppealInternInfoQuery,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAppealRepository _appealRepository;

        public GetAppealInternInfoQueryHandler(ICurrentRepository currentRepository, IAppealRepository appealRepository)
        {
            _currentRepository = currentRepository;
            _appealRepository = appealRepository;
        }

        public async Task<IResponse> Handle(GetAppealInternInfoQuery request, CancellationToken cancellationToken)
        {
            _currentRepository.AdminControl(_currentRepository.UserId());
            var internId= _appealRepository.GetAppealInternInfo(request.AppealId, _currentRepository.UserId());

            return new Response<Guid>(internId);
        }
    }
}