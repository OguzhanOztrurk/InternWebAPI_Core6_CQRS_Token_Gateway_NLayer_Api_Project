using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Intern.Talent.Queries;

public class GetTalentQuery:IRequest<IResponse>
{
    public class GetTalentQueryHandler:IRequestHandler<GetTalentQuery, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly ITalentRepository _talentRepository;

        public GetTalentQueryHandler(ICurrentRepository currentRepository, ITalentRepository talentRepository)
        {
            _currentRepository = currentRepository;
            _talentRepository = talentRepository;
        }

        public async Task<IResponse> Handle(GetTalentQuery request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());

            var talents =
                await _talentRepository.GetListAsync(x =>
                    x.InternId == _currentRepository.UserId() && x.DeleteDate == null);

            return new Response<IEnumerable<Entities.Concrete.Talent>>(talents);
        }
    }
}