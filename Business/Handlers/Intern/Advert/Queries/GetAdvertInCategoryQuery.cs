using Core.Wrappers;
using DataAccess.Abstract;
using Entities.Dto;
using MediatR;

namespace Business.Handlers.Intern.Advert.Queries;

public class GetAdvertInCategoryQuery:IRequest<IResponse>
{
    public int CategoryId { get; set; }
    public class GetAdvertInCategoryQueryHandler:IRequestHandler<GetAdvertInCategoryQuery,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAdvertRepository _advertRepository;

        public GetAdvertInCategoryQueryHandler(ICurrentRepository currentRepository, IAdvertRepository advertRepository)
        {
            _currentRepository = currentRepository;
            _advertRepository = advertRepository;
        }

        public async Task<IResponse> Handle(GetAdvertInCategoryQuery request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());

            var adverts = await _advertRepository.GetAdvertInCategoryList(request.CategoryId);

            return new Response<IEnumerable<ActiveAdvertListDTO>>(adverts);
        }
    }
}