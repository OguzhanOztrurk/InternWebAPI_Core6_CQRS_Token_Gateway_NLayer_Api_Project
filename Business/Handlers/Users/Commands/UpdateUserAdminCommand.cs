using Abp.Extensions;
using Core.Wrappers;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using MediatR;

namespace Business.Handlers.Users.Commands;

public class UpdateUserAdminCommand:IRequest<IResponse>
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Number { get; set; }
    public string? Email { get; set; }
    public string? Position { get; set; }
    
    public class UpdateUserAdminCommandHandler:IRequestHandler<UpdateUserAdminCommand,IResponse>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentRepository _currentRepository;

        public UpdateUserAdminCommandHandler(ICurrentRepository currentRepository, IUserRepository userRepository, IAdminRepository adminRepository)
        {
            _currentRepository = currentRepository;
            _userRepository = userRepository;
            _adminRepository = adminRepository;
        }

        public async Task<IResponse> Handle(UpdateUserAdminCommand request, CancellationToken cancellationToken)
        {
            _adminRepository.AdminControl(_currentRepository.UserId());
            _userRepository.UserNameControl(request.UserName);
            
            var updateUser = _userRepository.Get(x => x.UserId == _currentRepository.UserId());
            updateUser.UserName = request.UserName;
            updateUser.FirstName = request.FirstName;
            updateUser.LastName = request.LastName;
            updateUser.Number = request.Number;
            updateUser.Email = request.Email;
            
            _userRepository.Update(updateUser);
            await _userRepository.SaveChangesAsync();

            var updateAdmin = _adminRepository.Get(x => x.UserId == _currentRepository.UserId());
            updateAdmin.Position = request.Position;

            _adminRepository.Update(updateAdmin);
            await _userRepository.SaveChangesAsync();
            var result = new UserAdminInfoDTO()
            {
                UserName = updateUser.UserName,
                FirstName = updateUser.FirstName,
                LastName = updateUser.LastName,
                Number = updateUser.Number,
                Email = updateUser.Email,
                Position = updateAdmin.Position
            };
            return new Response<UserAdminInfoDTO>(result);
        }
    }
}