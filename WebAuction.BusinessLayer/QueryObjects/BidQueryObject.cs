using System;
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
    public class BidQueryObject : QueryObjectBase<BidDto, Bid, BidFilterDto, IQuery<Bid>>
    {
        public BidQueryObject(IMapper mapper, IQuery<Bid> query) : base(mapper, query)
        {
        }

        protected override IQuery<Bid> ApplyWhereClause(IQuery<Bid> query, BidFilterDto filter)
        {
            return filter.AuctionId.Equals(Guid.Empty)
                ? query
                : query.Where(new SimplePredicate(nameof(Bid.AuctionId), ValueComparingOperator.Equal, filter.AuctionId));
        }
    }
}
