using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Advert.Command;

public class DeleteAdvertCommand:IRequest<IResponse>
{
    public int AdvertId { get; set; }
    public class DeleteAdvertCommandHandler:IRequestHandler<DeleteAdvertCommand,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAdvertRepository _advertRepository;

        public DeleteAdvertCommandHandler(ICurrentRepository currentRepository, IAdvertRepository advertRepository)
        {
            _currentRepository = currentRepository;
            _advertRepository = advertRepository;
        }

        public async Task<IResponse> Handle(DeleteAdvertCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.AdminControl(_currentRepository.UserId());
            _advertRepository.AdvertControl(request.AdvertId);
            _advertRepository.AdvertWorkplaceActive(request.AdvertId);
            _advertRepository.WorkplaceAdvertControl(request.AdvertId, _currentRepository.UserId());

            var advert = await _advertRepository.GetAsync(x => x.AdvertId == request.AdvertId);
            advert.DeleteDate=DateTime.Now;
            advert.DeleteUserId = _currentRepository.UserId();

            _advertRepository.Update(advert);
            await _advertRepository.SaveChangesAsync();

            return new Response<object>(null, "Ad has been deleted");

        }
    }
}