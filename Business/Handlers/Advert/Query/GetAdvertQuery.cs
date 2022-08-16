using Core.Wrappers;
using DataAccess.Abstract;
using Entities.Dto;
using MediatR;

namespace Business.Handlers.Advert.Query;

public class GetAdvertQuery:IRequest<IResponse>
{
    public int WorkplaceId { get; set; }  
    public class GetAdvertQueryHandler:IRequestHandler<GetAdvertQuery, IResponse>
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly ICurrentRepository _currentRepository;

        public GetAdvertQueryHandler(IAdvertRepository advertRepository, ICurrentRepository currentRepository)
        {
            _advertRepository = advertRepository;
            _currentRepository = currentRepository;
        }

        public async Task<IResponse> Handle(GetAdvertQuery request, CancellationToken cancellationToken)
        {
            _currentRepository.AdminControl(_currentRepository.UserId());
            _advertRepository.AdvertWorkplaceControl(request.WorkplaceId);
            _advertRepository.WorkplaceControl(request.WorkplaceId);
            _advertRepository.AdminWorkplaceControl(request.WorkplaceId, _currentRepository.UserId());
            
            
            var result = await _advertRepository.GetAdvertList(request.WorkplaceId);
            return new Response<IEnumerable<AdvertWithAdvertDetailDTO>>(result);
        }
    }
}