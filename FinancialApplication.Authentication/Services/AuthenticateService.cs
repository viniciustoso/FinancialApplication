using FinancialApplication.Authentication.Models;
using FinancialApplication.Authentication.Persistences.MongoDB;
using FinancialApplication.Utility.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinancialApplication.Authentication.Services
{
    public class AuthenticateService
    {
        readonly IUsersRepository UsersRepository;

        public AuthenticateService(
            IUsersRepository usersRepository
        )
        {
            UsersRepository = usersRepository;
        }

        public async Task<JwtTokenModel> Authenticate(UserModel user)
        {
            UsersDBModel userDB = await UsersRepository.GetUserIdAsync(user.User);
            ValidateUser(user, userDB);
            return CreateToken(userDB);
        }

        private void ValidateUser(UserModel user, UsersDBModel userDB)
        {
            if (userDB == null || userDB.Password != user.Password)
                throw new UnauthorizedAccessException("Username or password is incorrect.");

            if (userDB.IsSuspended)
                throw new UnauthorizedAccessException("The user is temporarily suspended. Please contact the administrator.");
        }

        private JwtTokenModel CreateToken(UsersDBModel userDB)
        {
            DateTime createDate = DateTime.UtcNow.ToLocalTime();
            DateTime expirationDate = createDate.AddMinutes(userDB.LifecycleMinutes);

            byte[] key = Encoding.ASCII.GetBytes(userDB.PasswordToken);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = CreateClaimsIdentity(userDB),
                NotBefore = createDate,
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            return new JwtTokenModel
            {
                Authenticated = true,
                Created = createDate,
                Expiration = expirationDate,
                AccessToken = token
            };
        }

        private ClaimsIdentity CreateClaimsIdentity(UsersDBModel userDB)
        {
            return new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, userDB.UserId),
            });
        }
    }
}
