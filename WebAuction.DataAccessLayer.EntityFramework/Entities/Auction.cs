using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAuction.DataAccessLayer.EntityFramework.Validation;
using WebAuction.Infrastructure;

namespace WebAuction.DataAccessLayer.EntityFramework.Entities
{
    public class Auction : IEntity
    {
        public string TableName { get; set; } = nameof(WebAuctionDbContext.Auctions);

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public string Description { get; set; }

        [MaxLength(1024)]
        public string PhotoUri { get; set; }

        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [ForeignKey(nameof(Seller))]
        public Guid SellerId { get; set; }

        public virtual User Seller { get; set; }
        [PositiveDecimal]
        public decimal StartingPrice { get; set; }

        [PositiveDecimal]
        public decimal ActualPrice { get; set; }

        [PositiveDecimal]
        public decimal MinimalBid { get; set; }

        [PositiveDecimal]
        public decimal BuyoutPrice { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public DateTime? SellTime { get; set; }

        public string HandoverOptions { get; set; }

        public bool Ended { get; set; }

        [Range(0, int.MaxValue)]
        public int DisplayCount { get; set; }
    }
}
