using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Intern.AdvertCategory.Queries;

public class GetCategoriesQuery:IRequest<IResponse>
{
    public class GetCategoriesQueryHandler:IRequestHandler<GetCategoriesQuery, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAdvertCategoryRepository _advertCategoryRepository;

        public GetCategoriesQueryHandler(ICurrentRepository currentRepository, IAdvertCategoryRepository advertCategoryRepository)
        {
            _currentRepository = currentRepository;
            _advertCategoryRepository = advertCategoryRepository;
        }

        public async Task<IResponse> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());

            var categories = await _advertCategoryRepository.GetListAsync();
            return new Response<IEnumerable<Entities.Concrete.AdvertCategory>>(categories);
        }
    }
}