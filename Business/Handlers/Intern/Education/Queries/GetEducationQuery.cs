using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Intern.Education.Queries;

public class GetEducationQuery:IRequest<IResponse>
{
    public class GetEducationQueryHandler:IRequestHandler<GetEducationQuery,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IEducationRepository _educationRepository;

        public GetEducationQueryHandler(ICurrentRepository currentRepository, IEducationRepository educationRepository)
        {
            _currentRepository = currentRepository;
            _educationRepository = educationRepository;
        }

        public async Task<IResponse> Handle(GetEducationQuery request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());

            var educations = await _educationRepository.GetListAsync(x =>
                x.InternId == _currentRepository.UserId() && x.DeleteDate == null);

            return new Response<IEnumerable<Entities.Concrete.Education>>(educations);
        }
    }
}