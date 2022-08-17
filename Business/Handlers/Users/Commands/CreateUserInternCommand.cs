using System.Security.Cryptography;
using Core.Wrappers;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Users.Commands;

public class CreateUserInternCommand:IRequest<IResponse>
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PassWord { get; set; }
    public class CreateUserInternCommandHandler:IRequestHandler<CreateUserInternCommand,IResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IInternRepository _internRepository;

        public CreateUserInternCommandHandler(IUserRepository userRepository, IInternRepository internRepository)
        {
            _userRepository = userRepository;
            _internRepository = internRepository;
        }

        public async Task<IResponse> Handle(CreateUserInternCommand request, CancellationToken cancellationToken)
        {
            _userRepository.UserNameControl(request.UserName);
            CreatePasswordHash(request.PassWord, out var passwordHash, out var passwordSalt);
            var newUser = new User();
            newUser.UserName = request.UserName;
            newUser.FirstName = request.FirstName;
            newUser.LastName = request.LastName;
            newUser.isActive = true;
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;
            
            _userRepository.Add(newUser);
            await _userRepository.SaveChangesAsync();
            
            
            var newIntern = new Intern();
            newIntern.UserId = newUser.UserId;
            _internRepository.Add(newIntern);
            await _internRepository.SaveChangesAsync();
            
            return new Response<User>(newUser);
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