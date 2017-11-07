using System.Collections.Generic;
using WebAuction.BusinessLayer.DataTransferObjects.Common;

namespace WebAuction.BusinessLayer.DataTransferObjects
{
    public class AuctionBidsDto : DtoBase
    {
        public AuctionDto Auction { get; set; }

        public IEnumerable<BidDto> Bids { get; set; } = new List<BidDto>();
    }
}
