using System;
using WebAuction.BusinessLayer.DataTransferObjects.Common;

namespace WebAuction.BusinessLayer.DataTransferObjects.Filters
{
    public class AuctionFilterDto : FilterDtoBase
    {
        public string Name { get; set; }

        public Guid SellerId { get; set; }

        public Guid[] CategoryIds { get; set; }

        public decimal MaximalActualPrice { get; set; } = decimal.MaxValue;
    }
}
