using Core.Wrappers;
using DataAccess.Abstract;
using Entities.Dto;
using MediatR;

namespace Business.Handlers.Intern.AdvertDetail.Query;

public class GetAdvertDetailQuery:IRequest<IResponse>
{
    public int AdvertId { get; set; }
    
    public class GetAdvertDetailQueryHandler:IRequestHandler<GetAdvertDetailQuery,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAdvertDetailRepository _advertDetailRepository;

        public GetAdvertDetailQueryHandler(ICurrentRepository currentRepository, IAdvertDetailRepository advertDetailRepository)
        {
            _currentRepository = currentRepository;
            _advertDetailRepository = advertDetailRepository;
        }

        public async Task<IResponse> Handle(GetAdvertDetailQuery request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());

            var result = await _advertDetailRepository.AdvertDetailList(request.AdvertId);
            
            return new Response<ActiveAdvertDetailListDTO>(result);
        }
    }
}