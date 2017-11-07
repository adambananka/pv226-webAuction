using System;
using WebAuction.BusinessLayer.DataTransferObjects.Common;

namespace WebAuction.BusinessLayer.DataTransferObjects
{
    public class PaymentDto : DtoBase
    {
        public Guid BuyerId { get; set; }

        public Guid AuctionId { get; set; }

        public decimal Amount { get; set; }

        public DateTime Time { get; set; }

        public string CreditCardNumber { get; set; }
    }
}
