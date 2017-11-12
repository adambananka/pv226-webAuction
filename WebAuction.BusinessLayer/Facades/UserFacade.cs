using System;
using System.Threading.Tasks;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Common;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using WebAuction.BusinessLayer.Facades.Common;
using WebAuction.BusinessLayer.Services.Users;
using WebAuction.Infrastructure.UnitOfWork;

namespace WebAuction.BusinessLayer.Facades
{
    public class UserFacade : FacadeBase
    {
        private readonly IUserService _userService;

        public UserFacade(IUnitOfWorkProvider unitOfWorkProvider, IUserService userService) : base(unitOfWorkProvider)
        {
            _userService = userService;
        }

        public async Task<UserDto> GetUserAccordingToEmailAsync(string email)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _userService.GetUserAccordingToEmailAsync(email);
            }
        }

        public async Task<QueryResultDto<UserDto, UserFilterDto>> GetAllUsersAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _userService.ListAllAsync();
            }
        }

        ///// <summary>
        ///// Performs customer registration
        ///// </summary>
        ///// <param name="registrationDto">Customer registration details</param>
        ///// <returns>Registered customer account ID, empty if unsuccessful</returns>
        public async Task<Guid> RegisterUser(UserCompleteDto registrationDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (_userService.GetUserAccordingToEmailAsync(registrationDto.Email) != null)
                {
                    return Guid.Empty;
                }
                var accountId = _userService.CreateCustomer(registrationDto);
                await uow.Commit();
                return accountId;
            }
        }

        public async Task<bool> EditUser(UserDto userDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await _userService.GetAsync(userDto.Id, false) == null)
                {
                    return false;
                }
                await _userService.Update(userDto);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await _userService.GetAsync(userId, false) == null)
                {
                    return false;
                }
                _userService.Delete(userId);
                await uow.Commit();
                return true;
            }
        }
    }
}