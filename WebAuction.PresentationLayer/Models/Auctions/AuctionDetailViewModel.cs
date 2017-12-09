using System.Collections.Generic;
using WebAuction.BusinessLayer.DataTransferObjects;

namespace WebAuction.PresentationLayer.Models.Auctions
{
    public class AuctionDetailViewModel
    {
        public AuctionDto Auction { get; set; }
        public BidDto NewBid { get; set; }
        public IList<BidDto> Bids { get; set; }
        public IList<CommentDto> Comments { get; set; }
    }
}