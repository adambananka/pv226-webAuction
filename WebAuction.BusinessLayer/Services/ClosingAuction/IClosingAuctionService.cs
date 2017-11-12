using WebAuction.BusinessLayer.DataTransferObjects;

namespace WebAuction.BusinessLayer.Services.ClosingAuction
{
    public interface IClosingAuctionService
    {
        void CloseAuctionDueToTimeout(AuctionDto auctionDto, BidDto[] auctionBids);

        void CloseAuctionDueToBuyout(AuctionDto auctionDto, BidDto[] auctionBids);
    }
}
