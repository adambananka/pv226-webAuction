using AutoMapper;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using WebAuction.BusinessLayer.QueryObjects.Common;
using WebAuction.DataAccessLayer.EntityFramework.Entities;
using WebAuction.Infrastructure.Query;
using WebAuction.Infrastructure.Query.Predicates;
using WebAuction.Infrastructure.Query.Predicates.Operators;

namespace WebAuction.BusinessLayer.QueryObjects
{
    public class UserLoginQueryObject : QueryObjectBase<UserLoginDto, UserLogin, UserLoginFilterDto, IQuery<UserLogin>>
    {
        public UserLoginQueryObject(IMapper mapper, IQuery<UserLogin> query) : base(mapper, query) {}

        protected override IQuery<UserLogin> ApplyWhereClause(IQuery<UserLogin> query, UserLoginFilterDto filter)
        {
            return
                query.Where(new SimplePredicate(nameof(UserLogin.Username), ValueComparingOperator.Equal,
                    filter.Username));
        }
    }
}
