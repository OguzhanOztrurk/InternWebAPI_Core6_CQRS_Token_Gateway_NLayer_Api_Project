using Core.Wrappers;
using DataAccess.Abstract;
using Entities.Dto;
using MediatR;

namespace Business.Handlers.Admin.Appeal.Query;

public class GetAppealQuery:IRequest<IResponse>
{
    public class GetAppealQueryHandler:IRequestHandler<GetAppealQuery,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAppealRepository _appealRepository;

        public GetAppealQueryHandler(ICurrentRepository currentRepository, IAppealRepository appealRepository)
        {
            _currentRepository = currentRepository;
            _appealRepository = appealRepository;
        }

        public async Task<IResponse> Handle(GetAppealQuery request, CancellationToken cancellationToken)
        {
            _currentRepository.AdminControl(_currentRepository.UserId());

            var appeals = await _appealRepository.GetAdminAppealList(_currentRepository.UserId());

            return new Response<IEnumerable<AppealAdminListDTO>>(appeals);
        }
    }
}