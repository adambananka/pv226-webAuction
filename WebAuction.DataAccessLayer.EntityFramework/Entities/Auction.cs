using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAuction.DataAccessLayer.EntityFramework.Entities
{
    public class Auction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Seller))]
        public Guid SellerId { get; set; }

        public virtual User Seller { get; set; }

        [ForeignKey(nameof(Item))]
        public Guid ItemId { get; set; }

        public virtual Item Item { get; set; }

        [Range(0, double.MaxValue)]
        public double StratingPrice { get; set; } //alebo dame iba decimal?

        [Range(0, double.MaxValue)]
        public double MinimalBid { get; set; }

        [Range(0, double.MaxValue)]
        public double BuyoutPrice { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string HandoverOptions { get; set; }

        public bool Sold { get; set; }

        [Range(0, int.MaxValue)]
        public int DisplayCount { get; set; }
    }
}
