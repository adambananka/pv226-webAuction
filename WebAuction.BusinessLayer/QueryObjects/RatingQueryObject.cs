using System;
using AutoMapper;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using WebAuction.BusinessLayer.QueryObjects.Common;
using WebAuction.DataAccessLayer.EntityFramework.Entities;
using WebAuction.Infrastructure.Query;
using WebAuction.Infrastructure.Query.Predicates;
using WebAuction.Infrastructure.Query.Predicates.Operators;

namespace WebAuction.BusinessLayer.QueryObjects
{
    public class RatingQueryObject : QueryObjectBase<RatingFilterDto, Rating, RatingFilterDto, IQuery<Rating>>
    {
        public RatingQueryObject(IMapper mapper, IQuery<Rating> query) : base(mapper, query) {}
        protected override IQuery<Rating> ApplyWhereClause(IQuery<Rating> query, RatingFilterDto filter)
        {
            if (filter.SellerId.Equals(Guid.Empty))
            {
                return query;
            }

            return
                query.Where(new SimplePredicate(nameof(Rating.SellerId), ValueComparingOperator.Equal, filter.SellerId));
        }
    }
}
