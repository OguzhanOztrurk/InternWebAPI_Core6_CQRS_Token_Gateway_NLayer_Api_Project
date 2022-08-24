using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Repository;

public class AdvertCategoryRepository:EfEntityRepositoryBase<AdvertCategory, AppDbContext>, IAdvertCategoryRepository
{
    private readonly IAdvertRepository _advertRepository;
    public AdvertCategoryRepository(AppDbContext context, IAdvertRepository advertRepository) : base(context)
    {
        _advertRepository = advertRepository;
    }

    

    public async Task<IEnumerable<CategoryInAdvertCountDTO>> GetCategoryList()
    {
        var result = await Query()
            .Select(x => new CategoryInAdvertCountDTO()
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                Categorydefinition = x.Categorydefinition,
                AdvertNumber = _advertRepository.GetCategoryInAdvertCount(x.CategoryId)
            }).ToListAsync();
        return result;
    }
}