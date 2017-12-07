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
using Auction = WebAuction.DataAccessLayer.EntityFramework.Entities.Auction;

namespace WebAuction.BusinessLayer.Services.Bids
{
    public class BidService : CrudQueryServiceBase<Bid, BidDto, BidFilterDto>, IBidService
    {
        private readonly IRepository<Auction> _auctionRepository;

        public BidService(IMapper mapper, IRepository<Bid> repository,
            QueryObjectBase<BidDto, Bid, BidFilterDto, IQuery<Bid>> query, IRepository<Auction> auctionRepository) : base(mapper, repository, query)
        {
            _auctionRepository = auctionRepository;
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

        public async Task<IEnumerable<BidDto>> GetBidsAccordingToBuyerAsync(Guid buyerId)
        {
            var queryResult = await Query.ExecuteQuery(new BidFilterDto { BuyerId = buyerId });
            return queryResult.Items;
        }

        public async Task PlaceBid(BidDto bidDto)
        {
            var bid = Mapper.Map<Bid>(bidDto);

            var auction = await _auctionRepository.GetAsync(bidDto.AuctionId);
            if (auction == null)
            {
                throw new ArgumentException("Bidding Service - PlacedBid() - Auction must not be null");
            }

            auction.ActualPrice = bid.NewItemPrice;
            _auctionRepository.Update(auction);
        }
    }
}
