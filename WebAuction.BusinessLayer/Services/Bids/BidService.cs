using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using WebAuction.BusinessLayer.QueryObjects.Common;
using WebAuction.BusinessLayer.Services.Common;
using WebAuction.DataAccessLayer.EntityFramework.Entities;
using WebAuction.Infrastructure;
using WebAuction.Infrastructure.Query;

namespace WebAuction.BusinessLayer.Services.Bids
{
    public class BidService : CrudQueryServiceBase<Bid, BidDto, BidFilterDto>, IBidService
    {
        public BidService(IMapper mapper, IRepository<Bid> repository,
            QueryObjectBase<BidDto, Bid, BidFilterDto, IQuery<Bid>> query) : base(mapper, repository, query)
        {
        }

        protected override async Task<Bid> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<IEnumerable<BidDto>> GetBidsAccordingToAuctionAsync(Guid auctionId)
        {
            var queryResult = await Query.ExecuteQuery(new BidFilterDto {AuctionId = auctionId});
            return queryResult.Items;
        }
    }
}