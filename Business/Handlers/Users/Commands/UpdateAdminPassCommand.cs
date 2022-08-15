using System.Security.Cryptography;
using System.Text;
using Core.Wrappers;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Users.Commands;

public class UpdateAdminPassCommand:IRequest<IResponse>
{
    public string Password { get; set; }
    public string NewPassword { get; set; }
    public string NewPasswordRety { get; set; }
    public class UpdateAdminPassCommandHandler:IRequestHandler<UpdateAdminPassCommand,IResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentRepository _currentRepository;
        private readonly IAdminRepository _adminRepository;

        public UpdateAdminPassCommandHandler( ICurrentRepository currentRepository, IUserRepository userRepository, IAdminRepository adminRepository)
        {
            _currentRepository = currentRepository;
            _userRepository = userRepository;
            _adminRepository = adminRepository;
        }

        public async Task<IResponse> Handle(UpdateAdminPassCommand request, CancellationToken cancellationToken)
        {
            _adminRepository.AdminControl(_currentRepository.UserId());
            var user = await _userRepository.GetAsync(x => x.UserId == _currentRepository.UserId());
            if (!VerifyPasswordHash(request.Password,user.PasswordHash,user.PasswordSalt))
            {
                throw new Exception("Wrong password.");
            }

            if (request.NewPassword!=request.NewPasswordRety)
            {
                throw new Exception("Passwords are not the same.");
            }
            CreatePasswordHash(request.NewPassword, out var passwordHash, out var passwordSalt);
            var updatePass = _userRepository.Get(x => x.UserId == _currentRepository.UserId());
            updatePass.PasswordHash = passwordHash;
            updatePass.PasswordSalt = passwordSalt;
            _userRepository.Update(updatePass);
            await _userRepository.SaveChangesAsync();
            return new Response<object>(null, "Password change successful.");
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}