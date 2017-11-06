using System;
using WebAuction.BusinessLayer.DataTransferObjects.Common;

namespace WebAuction.BusinessLayer.DataTransferObjects.Filters
{
    public class AuctionFilterDto : FilterDtoBase
    {
        public Guid SellerId { get; set; }
    }
}
