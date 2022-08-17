using Core.Wrappers;
using DataAccess.Abstract;
using Entities.Dto;
using MediatR;

namespace Business.Handlers.Advert.Command;

public class UpdateAdvertCommand:IRequest<IResponse>
{
    public int AdvertId { get; set; }
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
    
    public class UpdateAdvertCommandHandler:IRequestHandler<UpdateAdvertCommand,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAdvertRepository _advertRepository;
        private readonly IAdvertDetailRepository _advertDetailRepository;

        public UpdateAdvertCommandHandler(ICurrentRepository currentRepository, IAdvertRepository advertRepository, IAdvertDetailRepository advertDetailRepository)
        {
            _currentRepository = currentRepository;
            _advertRepository = advertRepository;
            _advertDetailRepository = advertDetailRepository;
        }

        public async Task<IResponse> Handle(UpdateAdvertCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.AdminControl(_currentRepository.UserId());
            _advertRepository.AdvertControl(request.AdvertId);
            _advertRepository.AdvertWorkplaceActive(request.AdvertId);
            _advertRepository.AdminWorkplaceControl(request.WorkplaceId,_currentRepository.UserId());

            var advert = await _advertRepository.GetAsync(x => x.AdvertId == request.AdvertId);
            advert.WorkplaceId = request.WorkplaceId;
            advert.CategoryId = request.CategoryId;
            advert.AdvertName = request.AdvertName;
            advert.AdvertSummary = request.AdvertSummary;
            advert.StartDate = request.StartDate;
            advert.EndDate = request.EndDate;
            advert.Quota = request.Quota;
            advert.UpdateDate=DateTime.Now;
            advert.UpdateUserId = _currentRepository.UserId();

            _advertRepository.Update(advert);
            await _advertRepository.SaveChangesAsync();

            var advertDetail = await _advertDetailRepository.GetAsync(x => x.AdvertId == request.AdvertId);
            advertDetail.CompanyInfo = request.CompanyInfo;
            advertDetail.WorkDefinition = request.WorkDefinition;
            advertDetail.Quality = request.Quality;
            advertDetail.WorkEnvironment = request.WorkEnvironment;
            advertDetail.WorkHour = request.WorkHour;
            advertDetail.Facilities = request.Facilities;
            advertDetail.Wage = request.Facilities;

            _advertDetailRepository.Update(advertDetail);
            await _advertDetailRepository.SaveChangesAsync();

            var result = await _advertRepository.GetAdvert(request.AdvertId);

            return new Response<AdvertWithAdvertDetailDTO>(result);
        }
    }
}