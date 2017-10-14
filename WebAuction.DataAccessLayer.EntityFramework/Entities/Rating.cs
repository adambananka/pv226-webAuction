using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAuction.DataAccessLayer.EntityFramework.Entities
{
    public class Rating
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [ForeignKey(nameof(Seller))]
        public Guid SellerId { get; set; }

        public virtual User Seller { get; set; }

        [ForeignKey(nameof(Author))]
        public Guid AuthorId { get; set; }

        public virtual User Author { get; set; }

        public DateTime Time { get; set; }

        [Required]
        [Range(0, 5)]
        public int Stars { get; set; }

        public string Feedback { get; set; }
    }
}
