using System.Security.Cryptography;
using System.Text;
using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Users.Commands;

public class UpdateInternPassCommand:IRequest<IResponse>
{
    public string Password { get; set; }
    public string NewPassword { get; set; }
    public string NewPasswordRety { get; set; }
    
    public class UpdateInternPassCommandHandler:IRequestHandler<UpdateInternPassCommand,IResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentRepository _currentRepository;

        public UpdateInternPassCommandHandler(IUserRepository userRepository, ICurrentRepository currentRepository)
        {
            _userRepository = userRepository;
            _currentRepository = currentRepository;
        }

        public async Task<IResponse> Handle(UpdateInternPassCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());
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