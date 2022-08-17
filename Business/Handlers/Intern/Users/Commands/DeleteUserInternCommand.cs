using System.Security.Cryptography;
using System.Text;
using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Users.Commands;

public class DeleteUserInternCommand:IRequest<IResponse>
{
    public string Password { get; set; }
    
    public class DeleteUserInternCommandHandler:IRequestHandler<DeleteUserInternCommand,IResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentRepository _currentRepository;
        private readonly IInternRepository _internRepository;

        public DeleteUserInternCommandHandler(IUserRepository userRepository, ICurrentRepository currentRepository, IInternRepository internRepository)
        {
            _userRepository = userRepository;
            _currentRepository = currentRepository;
            _internRepository = internRepository;
        }

        public async Task<IResponse> Handle(DeleteUserInternCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());
            var user = await _userRepository.GetAsync(x => x.UserId == _currentRepository.UserId());
            if (!VerifyPasswordHash(request.Password,user.PasswordHash,user.PasswordSalt))
            {
                throw new Exception("Wrong password.");
            }

            var deleteUser = _userRepository.Get(x => x.UserId == _currentRepository.UserId());
            deleteUser.DeleteDate=DateTime.Now;
            deleteUser.DeleteUserId = _currentRepository.UserId();
            _userRepository.Update(deleteUser);
            await _userRepository.SaveChangesAsync();
            
            return new Response<object>(null, "The user has been permanently deleted.");
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}