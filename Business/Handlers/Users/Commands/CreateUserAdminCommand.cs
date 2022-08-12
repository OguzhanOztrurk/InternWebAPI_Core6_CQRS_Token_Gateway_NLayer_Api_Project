using System.Security.Cryptography;
using Core.Wrappers;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Users.Commands;

public class CreateUserAdminCommand:IRequest<IResponse>
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PassWord { get; set; }
    
    public class CreateUserAdminCommandHandler:IRequestHandler<CreateUserAdminCommand,IResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;

        public CreateUserAdminCommandHandler(IUserRepository userRepository, IAdminRepository adminRepository)
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
        }


        public async Task<IResponse> Handle(CreateUserAdminCommand request, CancellationToken cancellationToken)
        {
            _userRepository.UserNameControl(request.UserName);
            CreatePasswordHash(request.PassWord, out var passwordHash, out var passwordSalt);
            var newUser = new User();
            newUser.UserName = request.UserName;
            newUser.FirstName = request.FirstName;
            newUser.LastName = request.LastName;
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;
            
            _userRepository.Add(newUser);
            await _userRepository.SaveChangesAsync();
            
            
            var newAdmin = new Admin();
            newAdmin.UserId = newUser.UserId;
            _adminRepository.Add(newAdmin);
            await _adminRepository.SaveChangesAsync();

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