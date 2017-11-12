using System.Collections.Generic;
using WebAuction.BusinessLayer.DataTransferObjects;

namespace WebAuction.BusinessLayer.Services.ClosingAuction
{
    public interface IClosingAuctionService
    {
        void CloseAuctionDueToTimeout(AuctionDto auctionDto, IEnumerable<BidDto> auctionBids);

        void CloseAuctionDueToBuyout(AuctionDto auctionDto, IEnumerable<BidDto> auctionBids);
    }
}
