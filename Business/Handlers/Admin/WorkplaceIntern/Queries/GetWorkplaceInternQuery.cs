using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Admin.WorkplaceIntern.Queries;

public class GetWorkplaceInternQuery:IRequest<IResponse>
{
    public class GetWorkplaceInternQueryHandler:IRequestHandler<GetWorkplaceInternQuery, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IWorkplaceInternRepository _workplaceInternRepository;

        public GetWorkplaceInternQueryHandler(ICurrentRepository currentRepository, IWorkplaceInternRepository workplaceInternRepository)
        {
            _currentRepository = currentRepository;
            _workplaceInternRepository = workplaceInternRepository;
        }

        public async Task<IResponse> Handle(GetWorkplaceInternQuery request, CancellationToken cancellationToken)
        {
            _currentRepository.AdminControl(_currentRepository.UserId());

            var workplaceInters = await _workplaceInternRepository.GetInternList(_currentRepository.UserId());

            return new Response<IEnumerable<Entities.Concrete.WorkplaceIntern>>(workplaceInters);

        }
    }
}