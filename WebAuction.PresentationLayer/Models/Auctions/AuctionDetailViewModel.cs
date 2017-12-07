using System.Collections.Generic;
using WebAuction.BusinessLayer.DataTransferObjects;

namespace WebAuction.PresentationLayer.Models.Auctions
{
    public class AuctionDetailViewModel
    {
        public AuctionDto Auction { get; set; }
        public IList<CommentDto> Comments { get; set; }
    }
}