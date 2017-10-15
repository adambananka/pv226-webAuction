using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAuction.DataAccessLayer.EntityFramework.Entities
{
    public class Payment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Buyer))]
        public Guid BuyerId { get; set; }

        public virtual User Buyer { get; set; }

        [ForeignKey(nameof(Auction))]
        public Guid AuctionId { get; set; }

        public virtual Auction Auction { get; set; }

        [Range(typeof(decimal), "0", "99999999999999")]
        public decimal Amount { get; set; }

        public DateTime Time { get; set; }

        [Range(100000000000, 9999999999999999)]
        public long CreditCardNumber { get; set; }
    }
}
