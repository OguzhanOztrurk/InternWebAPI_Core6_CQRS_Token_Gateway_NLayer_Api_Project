using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Repository;

public class AdvertDetailRepository:EfEntityRepositoryBase<AdvertDetail, AppDbContext>,IAdvertDetailRepository
{

    private readonly IAdminRepository _adminRepository;
    private readonly IWorkplaceRepository _workplaceRepository;
    private readonly IAdvertRepository _advertRepository;
    public AdvertDetailRepository(AppDbContext context, IAdminRepository adminRepository, IWorkplaceRepository workplaceRepository, IAdvertRepository advertRepository) : base(context)
    {
        _adminRepository = adminRepository;
        _workplaceRepository = workplaceRepository;
        _advertRepository = advertRepository;
    }

    

    public async Task<ActiveAdvertDetailListDTO> AdvertDetailList(int advertId)
    {
        var result = await (from advertDetail in Context.AdvertDetails
                join advert in Context.Adverts on advertDetail.AdvertId equals advert.AdvertId
                join workplace in Context.Workplaces on advert.WorkplaceId equals workplace.WorkplaceId
                join user in Context.Users on workplace.AdminId equals user.UserId
                join advertCategory in Context.AdvertCategories on advert.CategoryId equals advertCategory.CategoryId
                where advertDetail.AdvertId == advertId &&
                      advert.isActive == true &&
                      advert.DeleteDate == null &&
                      workplace.isActive == true &&
                      workplace.DeleteDate == null &&
                      user.isActive == true &&
                      user.DeleteDate == null &&
                      advert.StartDate <= DateTime.Now &&
                      advert.EndDate >= DateTime.Now

                select new ActiveAdvertDetailListDTO()
                {
                    AdvertId = advert.AdvertId,
                    CategoryId = advert.CategoryId,
                    WorkplaceId = advert.WorkplaceId,
                    WorkplaceName = workplace.WorkplaceName,
                    CategoryName = advertCategory.CategoryName,
                    AdvertName = advert.AdvertName,
                    AdvertSummary = advert.AdvertSummary,
                    StartDate = advert.StartDate,
                    EndDate = advert.EndDate,
                    Quota = advert.Quota,
                    CreatedDate = advert.CreatedDate,
                    CompanyInfo = advertDetail.CompanyInfo,
                    WorkDefinition = advertDetail.WorkDefinition,
                    WorkEnvironment = advertDetail.WorkEnvironment,
                    WorkHour = advertDetail.WorkHour,
                    Wage = advertDetail.Wage,
                    Quality = advertDetail.Quality,
                    Facilities = advertDetail.Facilities
                }
            ).FirstAsync();

        return result;
    }

   

   
}