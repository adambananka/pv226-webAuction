using System;
using WebAuction.BusinessLayer.DataTransferObjects.Common;

namespace WebAuction.BusinessLayer.DataTransferObjects.Filters
{
    public class BidFilterDto : FilterDtoBase
    {
        public Guid AuctionId { get; set; }
    }
}
