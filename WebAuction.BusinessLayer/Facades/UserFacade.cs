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

        public async Task<QueryResultDto<UserDto, UserFilterDto>> GetAllUserAsync()
        {
            using (UnitOfWorkProvider)
            {
                return await _userService.ListAllAsync();
            }
        }

        ///// <summary>
        ///// Performs customer registration
        ///// </summary>
        ///// <param name="registrationDto">Customer registration details</param>
        ///// <param name="success">argument that tells whether the registration was successful</param>
        ///// <returns>Registered customer account ID</returns>
        public Guid RegisterCustomer(UserCompleteDto registrationDto, out bool success)
        {
            if (_userService.GetUserAccordingToEmailAsync(registrationDto.Email) != null)
            {
                success = false;
                return new Guid();
            }
            var accountId = 
            _userService.CreateCustomer(accountId);
            success = true;
            return accountId;
        }
    }
}
