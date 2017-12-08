using System;
using WebAuction.BusinessLayer.DataTransferObjects.Common;
using WebAuction.DataAccessLayer.EntityFramework.Validation;

namespace WebAuction.BusinessLayer.DataTransferObjects
{
    public class BidDto : DtoBase
    {
        public Guid AuctionId { get; set; }

        public Guid BuyerId { get; set; }

        public DateTime Time { get; set; }

        [PositiveDecimal]
        public decimal BidAmount { get; set; }

        public decimal NewItemPrice { get; set; }

        public override string ToString()
        {
            return $"Bid amount: {BidAmount}, placed by: {BuyerId}";
        }

        public override bool Equals(object obj)
        {
            var other = obj as BidDto;

            return other != null && Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
