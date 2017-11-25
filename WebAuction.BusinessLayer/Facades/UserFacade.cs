using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.Facades.Common;
using WebAuction.BusinessLayer.Services.UserLogins;
using WebAuction.BusinessLayer.Services.Users;
using WebAuction.Infrastructure.UnitOfWork;

namespace WebAuction.BusinessLayer.Facades
{
    public class UserFacade : FacadeBase
    {
        private readonly IUserService _userService;
        private readonly IUserLoginService _userLoginService;

        public UserFacade(IUnitOfWorkProvider unitOfWorkProvider, IUserService userService,
            IUserLoginService userLoginService) : base(unitOfWorkProvider)
        {
            _userService = userService;
            _userLoginService = userLoginService;
        }

        public async Task<UserDto> GetUserAccordingToEmailAsync(string email)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _userService.GetUserAccordingToEmailAsync(email);
            }
        }

        public async Task<UserDto> GetUserAccordingToUsernameAsync(string username)
        {
            using (UnitOfWorkProvider.Create())
            {
                var userLogin = await _userLoginService.GetUserAccordingToUsernameAsync(username);
                return await _userService.GetAsync(userLogin.Id);
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await _userService.ListAllAsync()).Items;
            }
        }

        ///// <summary>
        ///// Performs customer registration
        ///// </summary>
        ///// <param name="registrationDto">Customer registration details</param>
        ///// <returns>Registered customer account ID, empty if unsuccessful</returns>
        public async Task<Guid> RegisterUser(UserRegistrationDto registrationDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                try
                {
                    var accountId = await _userLoginService.RegisterUserLoginAsync(registrationDto);
                    await uow.Commit();
                    return accountId;
                } catch (ArgumentException)
                {
                    throw;
                }
            }
        }

        public async Task<(bool success, string roles)> Login(string username, string password)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _userLoginService.AuthorizeUserAsync(username, password);
            }
        }

        public async Task<UserLoginDto> GetUserLoginAccordingToUsernameAsync(string username)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _userLoginService.GetUserAccordingToUsernameAsync(username);
            }
        }
    }
}
