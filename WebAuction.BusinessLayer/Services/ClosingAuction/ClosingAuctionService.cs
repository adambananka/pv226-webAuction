using System;
using System.Collections.Generic;
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

        public void CloseAuctionDueToTimeout(AuctionDto auctionDto, IEnumerable<BidDto> auctionBids)
        {
            var auction = Mapper.Map<Auction>(auctionDto);
            if (auctionBids.Any())
            {
                auction.SellTime = auction.EndTime;
            }
            auction.Ended = true;
            _auctionRepository.Update(auction);
        }

        public void CloseAuctionDueToBuyout(AuctionDto auctionDto, IEnumerable<BidDto> auctionBids)
        {
            if (!auctionBids.Any())
            {
                throw new ArgumentException("ClosingAuctionService - CloseAuctionDueToBuyout(...) - auctionBids must contain at least one bid.");
            }
            var lastBid = auctionBids.OrderByDescending(bid => bid.Time).First();
            if (lastBid.NewItemPrice != auctionDto.BuyoutPrice)
            {
                throw new ArgumentException("ClosingAuctionService - CloseAuctionDueToBuyout(...) - auctionBids must contain buyout bid");
            }
            var auction = Mapper.Map<Auction>(auctionDto);
            auction.Ended = true;
            auction.SellTime = lastBid.Time;
            _auctionRepository.Update(auction);
        }
    }
}