using Core.Wrappers;
using DataAccess.Abstract;
using Entities.Dto;
using MediatR;

namespace Business.Handlers.Users.Queries;

public class GetUserAdminQuery:IRequest<IResponse>
{
    public class GetUserAdminQueryHandler:IRequestHandler<GetUserAdminQuery,IResponse>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ICurrentRepository _currentRepository;

        public GetUserAdminQueryHandler(IAdminRepository adminRepository, ICurrentRepository currentRepository)
        {
            _adminRepository = adminRepository;
            _currentRepository = currentRepository;
        }

        public async Task<IResponse> Handle(GetUserAdminQuery request, CancellationToken cancellationToken)
        {
            _adminRepository.AdminControl(_currentRepository.UserId());
            var admin = await _adminRepository.GetUserAdminInfo(_currentRepository.UserId());
            return new Response<UserAdminInfoDTO>(admin);
        }
    }
}