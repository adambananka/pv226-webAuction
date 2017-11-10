using System;
using System.Threading.Tasks;
using AutoMapper;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.Services.Common;
using WebAuction.DataAccessLayer.EntityFramework.Entities;
using WebAuction.Infrastructure;

namespace WebAuction.BusinessLayer.Services.Bidding
{
    public class BiddingService : ServiceBase, IBiddingService
    {
        private readonly IRepository<Auction> _auctionRepository;

        public BiddingService(IMapper mapper, IRepository<Auction> auctionRepository) : base(mapper)
        {
            _auctionRepository = auctionRepository;
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
