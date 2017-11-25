using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using WebAuction.BusinessLayer.QueryObjects.Common;
using WebAuction.BusinessLayer.Services.Common;
using WebAuction.DataAccessLayer.EntityFramework.Entities;
using WebAuction.Infrastructure;
using WebAuction.Infrastructure.Query;

namespace WebAuction.BusinessLayer.Services.UserLogins
{
    public class UserLoginService : ServiceBase, IUserLoginService
    {
        private const int Pbkdf2IterCount = 100000;
        private const int Pbkdf2SubkeyLength = 160 / 8;
        private const int SaltSize = 128 / 8;

        private readonly IRepository<User> _userRepository;

        private readonly QueryObjectBase<UserLoginDto, UserLogin, UserLoginFilterDto, IQuery<UserLogin>>
            _userLoginQueryObjectBase;

        public UserLoginService(IMapper mapper, IRepository<User> userRepository,
            QueryObjectBase<UserLoginDto, UserLogin, UserLoginFilterDto, IQuery<UserLogin>> userLoginQueryObjectBase)
            : base(mapper)
        {
            _userRepository = userRepository;
            _userLoginQueryObjectBase = userLoginQueryObjectBase;
        }

        public async Task<UserLoginDto> GetUserAccordingToUsernameAsync(string username)
        {
            var queryResult = await _userLoginQueryObjectBase.ExecuteQuery(new UserLoginFilterDto {Username = username});
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<Guid> RegisterUserLoginAsync(UserRegistrationDto userDto)
        {
            var customer = Mapper.Map<User>(userDto);

            if (await GetIfUserExistsAsync(customer.Username))
            {
                throw new ArgumentException();
            }

            var password = CreateHash(userDto.Password);
            customer.PasswordHash = password.Item1;
            customer.PasswordSalt = password.Item2;

            _userRepository.Create(customer);

            return customer.Id;
        }

        public async Task<(bool success, string roles)> AuthorizeUserAsync(string username, string password)
        {
            var userResult = await _userLoginQueryObjectBase.ExecuteQuery(new UserLoginFilterDto {Username = username});
            var user = userResult.Items.SingleOrDefault();

            var succ = user != null && VerifyHashedPassword(user.PasswordHash, user.PasswordSalt, password);
            var roles = user?.Roles ?? "";
            return (succ, roles);
        }

        private async Task<bool> GetIfUserExistsAsync(string username)
        {
            var queryResult = await _userLoginQueryObjectBase.ExecuteQuery(new UserLoginFilterDto {Username = username});
            return (queryResult.Items.Count() == 1);
        }

        private bool VerifyHashedPassword(string hashedPassword, string salt, string password)
        {
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            var saltBytes = Convert.FromBase64String(salt);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, Pbkdf2IterCount))
            {
                var generatedSubkey = deriveBytes.GetBytes(Pbkdf2SubkeyLength);
                return hashedPasswordBytes.SequenceEqual(generatedSubkey);
            }
        }

        private Tuple<string, string> CreateHash(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, Pbkdf2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(Pbkdf2SubkeyLength);

                return Tuple.Create(Convert.ToBase64String(subkey), Convert.ToBase64String(salt));
            }
        }
    }
}
