using Core.Wrappers;
using DataAccess.Abstract;
using Entities.Dto;
using MediatR;

namespace Business.Handlers.Intern.Workplace.Queries;

public class GetWorkplacesQuery:IRequest<IResponse>
{
    public class GetWorkplacesQueryHandler:IRequestHandler<GetWorkplacesQuery,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IWorkplaceRepository _workplaceRepository;

        public GetWorkplacesQueryHandler(ICurrentRepository currentRepository, IWorkplaceRepository workplaceRepository)
        {
            _currentRepository = currentRepository;
            _workplaceRepository = workplaceRepository;
        }

        public async Task<IResponse> Handle(GetWorkplacesQuery request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());

            var workplaces = await _workplaceRepository.GetWorkplaceAndAdminControl();

            return new Response<IEnumerable<WorkplaceDTO>>(workplaces);
        }
    }
}