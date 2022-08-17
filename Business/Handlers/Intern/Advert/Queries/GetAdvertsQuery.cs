using Core.Wrappers;
using DataAccess.Abstract;
using Entities.Dto;
using MediatR;

namespace Business.Handlers.Intern.Advert.Queries;

public class GetAdvertsQuery:IRequest<IResponse>
{
    public class GetAdvertsQueryHandler:IRequestHandler<GetAdvertsQuery, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAdvertRepository _advertRepository;

        public GetAdvertsQueryHandler(ICurrentRepository currentRepository, IAdvertRepository advertRepository)
        {
            _currentRepository = currentRepository;
            _advertRepository = advertRepository;
        }

        public async Task<IResponse> Handle(GetAdvertsQuery request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());
            var adverts = await _advertRepository.GetAdvertList();
            return new Response<IEnumerable<ActiveAdvertListDTO>>(adverts);
        }
    }
}