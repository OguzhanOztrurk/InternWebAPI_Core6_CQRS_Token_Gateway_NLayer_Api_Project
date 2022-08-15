using System.Security.Cryptography;
using System.Text;
using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Business.Handlers.Users.Commands;

public class DeleteUserAdminCommand:IRequest<IResponse>
{
    public string Password { get; set; }

    public class DeleteUserAdminCommandHandler:IRequestHandler<DeleteUserAdminCommand, IResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentRepository _currentRepository;
        private readonly IAdminRepository _adminRepository;

        public DeleteUserAdminCommandHandler(IUserRepository userRepository, ICurrentRepository currentRepository, IAdminRepository adminRepository)
        {
            _userRepository = userRepository;
            _currentRepository = currentRepository;
            _adminRepository = adminRepository;
        }

        public async Task<IResponse> Handle(DeleteUserAdminCommand request, CancellationToken cancellationToken)
        {
            _adminRepository.AdminControl(_currentRepository.UserId());
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