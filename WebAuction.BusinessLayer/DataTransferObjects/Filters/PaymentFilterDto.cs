using System;
using WebAuction.BusinessLayer.DataTransferObjects.Common;

namespace WebAuction.BusinessLayer.DataTransferObjects.Filters
{
    public class PaymentFilterDto : FilterDtoBase
    {
        public Guid AuctionId { get; set; }
    }
}
