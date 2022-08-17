using Core.Wrappers;
using DataAccess.Abstract;
using Entities.Dto;
using MediatR;

namespace Business.Handlers.Intern.Advert.Queries;

public class GetAdvertsInWorkplaceQuery:IRequest<IResponse>
{
    public int WorkplaceId { get; set; }
    public class GetAdvertsInWorkplaceQueryHandler:IRequestHandler<GetAdvertsInWorkplaceQuery,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAdvertRepository _advertRepository;

        public GetAdvertsInWorkplaceQueryHandler(ICurrentRepository currentRepository, IAdvertRepository advertRepository)
        {
            _currentRepository = currentRepository;
            _advertRepository = advertRepository;
        }

        public async Task<IResponse> Handle(GetAdvertsInWorkplaceQuery request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());

            var adverts = await _advertRepository.GetAdvertInWorkplaceList(request.WorkplaceId);
            
            return new Response<IEnumerable<ActiveAdvertListDTO>>(adverts);
        }
    }
}