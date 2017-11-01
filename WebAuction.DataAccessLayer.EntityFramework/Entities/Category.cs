using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAuction.DataAccessLayer.EntityFramework.Entities
{
    public class Category
    {
        [NotMapped]
        public string TableName { get; set; } = nameof(WebAuctionDbContext.Categories);

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [ForeignKey(nameof(Parent))]
        public int? ParentId { get; set; }

        public virtual Category Parent { get; set; }

        [NotMapped]
        public bool IsRootCategory => Parent == null;
    }
}
