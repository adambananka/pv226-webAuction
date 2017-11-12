using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using WebAuction.BusinessLayer.QueryObjects.Common;
using WebAuction.BusinessLayer.Services.Common;
using WebAuction.DataAccessLayer.EntityFramework.Entities;
using WebAuction.Infrastructure;
using WebAuction.Infrastructure.Query;

namespace WebAuction.BusinessLayer.Services.Users
{
    public class UserService : CrudQueryServiceBase<User, UserDto, UserFilterDto>, IUserService
    {
        public UserService(IMapper mapper, IRepository<User> repository,
            QueryObjectBase<UserDto, User, UserFilterDto, IQuery<User>> query) : base(mapper, repository, query)
        {
        }

        protected override async Task<User> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<UserDto> GetUserAccordingToEmailAsync(string email)
        {
            var queryResult = await Query.ExecuteQuery(new UserFilterDto {Email = email});
            return queryResult.Items.SingleOrDefault();
        }

        public Guid CreateCustomer(UserCompleteDto user)
        {
            var entity = Mapper.Map<User>(user);
            Repository.Create(entity);
            return entity.Id;
        }
    }
}