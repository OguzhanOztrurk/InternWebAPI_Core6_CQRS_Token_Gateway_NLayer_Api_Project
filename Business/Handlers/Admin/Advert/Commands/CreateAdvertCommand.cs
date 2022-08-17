using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

using Entities.Concrete;
using Entities.Dto;

namespace Business.Handlers.Advert.Command;

public class CreateAdvertCommand:IRequest<IResponse>
{
    public int WorkplaceId { get; set; }
    public int CategoryId { get; set; }
    public string AdvertName { get; set; }
    public string AdvertSummary { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Quota { get; set; }
    
    public string CompanyInfo { get; set; }
    public string WorkDefinition { get; set; }
    public string Quality { get; set; }
    public string WorkEnvironment { get; set; }
    public string WorkHour { get; set; }
    public string Facilities { get; set; }
    public string Wage { get; set; }

    public class CreateAdvertCommandHandler:IRequestHandler<CreateAdvertCommand,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAdvertRepository _advertRepository;
        private readonly IAdvertDetailRepository _advertDetailRepository;


        public CreateAdvertCommandHandler(ICurrentRepository currentRepository, IAdvertRepository advertRepository, IAdvertDetailRepository advertDetailRepository)
        {
            _currentRepository = currentRepository;
            _advertRepository = advertRepository;
            _advertDetailRepository = advertDetailRepository;
        }

        public async Task<IResponse> Handle(CreateAdvertCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.AdminControl(_currentRepository.UserId());
            _advertRepository.WorkplaceControl(request.WorkplaceId);
            _advertRepository.AdminWorkplaceControl(request.WorkplaceId, _currentRepository.UserId());

            var advert = new Entities.Concrete.Advert();
            advert.WorkplaceId = request.WorkplaceId;
            advert.CategoryId = request.CategoryId;
            advert.AdvertName = request.AdvertName;
            advert.AdvertSummary = request.AdvertSummary;
            advert.StartDate = request.StartDate;
            advert.EndDate = request.EndDate;
            advert.Quota = request.Quota;
            advert.isActive = true;
            advert.CreatedDate=DateTime.Now;
            advert.CreatedUserId = _currentRepository.UserId();


            _advertRepository.Add(advert);
            await _advertRepository.SaveChangesAsync();
            
            

            var advertDetail = new AdvertDetail();
            advertDetail.AdvertId = advert.AdvertId;
            advertDetail.CompanyInfo = request.CompanyInfo;
            advertDetail.WorkDefinition = request.WorkDefinition;
            advertDetail.Quality = request.Quality;
            advertDetail.WorkEnvironment = request.WorkEnvironment;
            advertDetail.WorkHour = request.WorkHour;
            advertDetail.Facilities = request.Facilities;
            advertDetail.Wage = request.Wage;


            _advertDetailRepository.Add(advertDetail);
            await _advertDetailRepository.SaveChangesAsync();
            

            var result = await _advertRepository.GetAdvert(advert.AdvertId);
            
            return new Response<AdvertWithAdvertDetailDTO>(result);
        }
    }
}