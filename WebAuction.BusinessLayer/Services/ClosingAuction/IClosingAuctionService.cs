using WebAuction.BusinessLayer.DataTransferObjects;

namespace WebAuction.BusinessLayer.Services.ClosingAuction
{
    public interface IClosingAuctionService
    {
        void CloseAuctionDueToTimeout(AuctionDto auctionDto);

        void CloseAuctionDueToBuyout(AuctionDto auctionDto);
    }
}
