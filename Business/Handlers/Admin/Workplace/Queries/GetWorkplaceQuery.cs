using Castle.Core.Internal;
using Core.Wrappers;
using DataAccess.Abstract;
using DataAccess.Concrete.Enum;
using MediatR;
using Entities.Concrete;


namespace Business.Handlers.Workplace.Queries;

public class GetWorkplaceQuery:IRequest<IResponse>
{
    public class GetWorkplaceQueryHandler:IRequestHandler<GetWorkplaceQuery,IResponse>
    {
        private readonly IWorkplaceRepository _workplaceRepository;
        private readonly ICurrentRepository _currentRepository;
        

        public GetWorkplaceQueryHandler(IWorkplaceRepository workplaceRepository, ICurrentRepository currentRepository)
        {
            _workplaceRepository = workplaceRepository;
            _currentRepository = currentRepository;
        }

        public async Task<IResponse> Handle(GetWorkplaceQuery request, CancellationToken cancellationToken)
        {
            _workplaceRepository.GetAdminControl(_currentRepository.UserId());
            var workplace = await _workplaceRepository.GetNotDeletedWorkplace(_currentRepository.UserId());
            if (workplace.IsNullOrEmpty())
            {
                throw new Exception("You don't have any businesses yet, add a business.");
            }

            return new Response<IEnumerable<Entities.Concrete.Workplace>>(workplace);
        }
    }
}