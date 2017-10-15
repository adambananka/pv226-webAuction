using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAuction.DataAccessLayer.EntityFramework.Entities
{
    public class Item
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [ForeignKey(nameof(Seller))]
        public Guid SellerId { get; set; }

        public virtual User Seller { get; set; }

        [ForeignKey(nameof(Auction))]
        public Guid AuctionId { get; set; }

        public virtual Auction Auction { get; set; }

        public string Description { get; set; }
    }
}
