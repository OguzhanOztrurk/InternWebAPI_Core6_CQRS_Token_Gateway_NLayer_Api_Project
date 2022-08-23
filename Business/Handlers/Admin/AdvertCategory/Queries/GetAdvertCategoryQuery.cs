using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Admin.AdvertCategory.Queries;

public class GetAdvertCategoryQuery:IRequest<IResponse>
{
    public class GetAdvertCategoryQueryHandler:IRequestHandler<GetAdvertCategoryQuery, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAdvertCategoryRepository _advertCategoryRepository;

        public GetAdvertCategoryQueryHandler(ICurrentRepository currentRepository, IAdvertCategoryRepository advertCategoryRepository)
        {
            _currentRepository = currentRepository;
            _advertCategoryRepository = advertCategoryRepository;
        }

        public async Task<IResponse> Handle(GetAdvertCategoryQuery request, CancellationToken cancellationToken)
        {
            _currentRepository.AdminControl(_currentRepository.UserId());

            var categories = await _advertCategoryRepository.GetListAsync();

            return new Response<IEnumerable<Entities.Concrete.AdvertCategory>>(categories);
        }
    }
}