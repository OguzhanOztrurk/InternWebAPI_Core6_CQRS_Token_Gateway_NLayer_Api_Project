using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Core.Wrappers;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Business.Handlers.Users.Queries;

public class GetLoginUserInternQuery:IRequest<IResponse>
{
    
    public string UserName { get; set; }
    public string Password { get; set; }
    
    public class GetLoginUserInternQueryHandler:IRequestHandler<GetLoginUserInternQuery, IResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public GetLoginUserInternQueryHandler(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<IResponse> Handle(GetLoginUserInternQuery request, CancellationToken cancellationToken)
        {
            var newUser = await _userRepository.GetAsync(x => x.UserName == request.UserName);
            _userRepository.UserInternControl(newUser.UserId);

            if (newUser == null) return new Response<User>(null,  "User information is incorrect");
            else if (!VerifyPasswordHash(request.Password, newUser.PasswordHash, newUser.PasswordSalt))
                return new Response<User>(null,  "User information is incorrect");
            _userRepository.UserDeleteControl(newUser.UserId);
            var newToken = new TokenDTO();
            newToken.Token = CreateToken(newUser);
            return new Response<TokenDTO>(newToken);
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                _configuration["Jwt:ValidIssuer"],
                _configuration["Jwt:ValidAudience"],
                GetClaims(user),
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:TokenValidityInMinutes"])),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),

            };
            
            return claims;
        }
    }
}