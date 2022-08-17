using Core.Wrappers;
using DataAccess.Abstract;
using Entities.Dto;
using MediatR;

namespace Business.Handlers.Advert.Command;

public class StateAdvertCommand:IRequest<IResponse>
{
    public int AdvertId { get; set; }
    public class StateAdvertCommandHandler:IRequestHandler<StateAdvertCommand, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAdvertRepository _advertRepository;

        public StateAdvertCommandHandler(ICurrentRepository currentRepository, IAdvertRepository advertRepository)
        {
            _currentRepository = currentRepository;
            _advertRepository = advertRepository;
        }

        public async Task<IResponse> Handle(StateAdvertCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.AdminControl(_currentRepository.UserId());
            _advertRepository.AdvertControl(request.AdvertId);
            _advertRepository.AdvertWorkplaceActive(request.AdvertId);
            _advertRepository.WorkplaceAdvertControl(request.AdvertId,_currentRepository.UserId());

            var advert = await _advertRepository.GetAsync(x => x.AdvertId == request.AdvertId);
            advert.isActive = !advert.isActive;

            _advertRepository.Update(advert);
            await _advertRepository.SaveChangesAsync();

            var result = await _advertRepository.GetAdvert(request.AdvertId);
            
            return new Response<AdvertWithAdvertDetailDTO>(result);

        }
    }
}