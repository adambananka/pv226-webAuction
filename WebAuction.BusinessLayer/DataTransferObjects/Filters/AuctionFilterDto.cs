using System;
using WebAuction.BusinessLayer.DataTransferObjects.Common;

namespace WebAuction.BusinessLayer.DataTransferObjects.Filters
{
    public class AuctionFilterDto : FilterDtoBase
    {
        public string SearchedName { get; set; }

        public Guid SellerId { get; set; }

        public Guid[] CategoryIds { get; set; }

        public decimal MaximalActualPrice { get; set; } = decimal.MaxValue;

        public decimal MinimalActualPrice { get; set; } = decimal.MinValue;

        public DateTime MinimalEndTime = DateTime.MinValue;

        public DateTime MaximalEndTime = DateTime.MaxValue;
    }
}
