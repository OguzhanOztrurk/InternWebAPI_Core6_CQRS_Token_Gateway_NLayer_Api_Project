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

public class GetLoginAdminQuery:IRequest<IResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }
    
    public class GetLoginAdminQueryHandler:IRequestHandler<GetLoginAdminQuery,IResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public GetLoginAdminQueryHandler(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration; 
        }

        public async Task<IResponse> Handle(GetLoginAdminQuery request, CancellationToken cancellationToken)
        {
            var newUser = await _userRepository.GetAsync(x => x.UserName == request.UserName);

            if (newUser == null) return new Response<User>(null,561,  "Wrong Username");
            else if (!VerifyPasswordHash(request.Password, newUser.PasswordHash, newUser.PasswordSalt))
                return new Response<User>(newUser,422,  "Wrong password");
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