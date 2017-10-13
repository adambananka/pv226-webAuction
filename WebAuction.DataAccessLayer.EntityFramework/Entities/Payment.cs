using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAuction.DataAccessLayer.EntityFramework.Entities
{
    public class Payment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(User))]
        public Guid Userid { get; set; } //alebo Buyer?

        public virtual User User { get; set; }

        [ForeignKey(nameof(Auction))]
        public Guid AuctionId { get; set; } //toto sme nemali v navrhu, ale ako by sme vedeli za co ta platba je? :D

        public virtual Auction Auction { get; set; }

        [Range(0, double.MaxValue)]
        public double Amount { get; set; }

        public DateTime Time { get; set; }

        [Range(100000000000, 9999999999999999999)]
        public int CreditCardNumber { get; set; }
    }
}
