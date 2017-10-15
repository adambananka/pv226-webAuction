using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAuction.DataAccessLayer.EntityFramework.Entities
{
    public class Photo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [ForeignKey(nameof(Item))]
        public Guid ItemId { get; set; }

        public virtual Item Item { get; set; }

        [Required]
        [MaxLength(1024)]
        public string ImageUri { get; set; }
    }
}