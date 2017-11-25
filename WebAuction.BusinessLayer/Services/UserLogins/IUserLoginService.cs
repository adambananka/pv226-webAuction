using System;
using System.Threading.Tasks;
using WebAuction.BusinessLayer.DataTransferObjects;

namespace WebAuction.BusinessLayer.Services.UserLogins
{
    public interface IUserLoginService
    {
        Task<Guid> RegisterUserLoginAsync(UserRegistrationDto user);
        Task<(bool success, string roles)> AuthorizeUserAsync(string username, string password);
        Task<UserLoginDto> GetUserAccordingToUsernameAsync(string username);
    }
}
