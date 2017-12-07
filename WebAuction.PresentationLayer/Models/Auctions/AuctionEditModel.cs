using WebAuction.BusinessLayer.DataTransferObjects;

namespace WebAuction.PresentationLayer.Models.Auctions
{
    public class AuctionEditModel
    {
        public AuctionDto Auction { get; set; }
        public CategoryDto Category { get; set; }
    }
}