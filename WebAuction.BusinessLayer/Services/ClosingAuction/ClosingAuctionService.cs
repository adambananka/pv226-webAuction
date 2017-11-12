using System;
using System.Linq;
using AutoMapper;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.Services.Common;
using WebAuction.DataAccessLayer.EntityFramework.Entities;
using WebAuction.Infrastructure;

namespace WebAuction.BusinessLayer.Services.ClosingAuction
{
    public class ClosingAuctionService : ServiceBase, IClosingAuctionService
    {
        private readonly IRepository<Auction> _auctionRepository;

        public ClosingAuctionService(IMapper mapper, IRepository<Auction> auctionRepository) : base(mapper)
        {
            _auctionRepository = auctionRepository;
        }

        // todo - premysliet dto...ok?
        public void CloseAuctionDueToTimeout(AuctionDto auctionDto, BidDto[] auctionBids)
        {
            var auction = Mapper.Map<Auction>(auctionDto);
            if (auctionBids.Any())
            {
                auction.SellTime = auction.EndTime;
            }
            auction.Ended = true;
            _auctionRepository.Update(auction);
        }

        // todo - premysliet Dto...staci?
        public void CloseAuctionDueToBuyout(AuctionDto auctionDto, BidDto[] auctionBids)
        {
            if (!auctionBids.Any())
            {
                throw new ArgumentException("ClosingAuctionService - CloseAuctionDueToBuyout(...) - auctionBids must contain at least one bid.");
            }
            var buyoutBid = auctionBids.OrderByDescending(bid => bid.Time).First();
            if (buyoutBid.BidAmount != auctionDto.BuyoutPrice)
            {
                throw new ArgumentException("ClosingAuctionService - CloseAuctionDueToBuyout(...) - auctionBids must contain buyout bid");
            }
            var auction = Mapper.Map<Auction>(auctionDto);
            auction.Ended = true;
            auction.SellTime = buyoutBid.Time;
            _auctionRepository.Update(auction);
        }
    }
}