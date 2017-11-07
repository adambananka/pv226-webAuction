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
    public class AuctionQueryObject : QueryObjectBase<AuctionDto, Auction, AuctionFilterDto, IQuery<Auction>>
    {
        public AuctionQueryObject(IMapper mapper, IQuery<Auction> query) : base(mapper, query)
        {
        }

        protected override IQuery<Auction> ApplyWhereClause(IQuery<Auction> query, AuctionFilterDto filter)
        {
            return filter.SellerId.Equals(Guid.Empty)
                ? query
                : query.Where(new SimplePredicate(nameof(Auction.SellerId), ValueComparingOperator.Equal, filter.SellerId));
        }
    }
}
