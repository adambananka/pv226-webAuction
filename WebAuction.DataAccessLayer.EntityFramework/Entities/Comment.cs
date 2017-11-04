using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAuction.Infrastructure;

namespace WebAuction.DataAccessLayer.EntityFramework.Entities
{
    public class Comment : IEntity
    {
        [NotMapped]
        public string TableName { get; set; } = nameof(WebAuctionDbContext.Comments);

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Auction))]
        public Guid AuctionId { get; set; }

        public virtual Auction Auction { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime Time { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Text { get; set; }
    }
}
