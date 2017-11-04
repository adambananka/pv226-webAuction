using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAuction.Infrastructure;

namespace WebAuction.DataAccessLayer.EntityFramework.Entities
{
    public class Category : IEntity
    {
        [NotMapped]
        public string TableName { get; set; } = nameof(WebAuctionDbContext.Categories);

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [ForeignKey(nameof(Parent))]
        public Guid? ParentId { get; set; }

        public virtual Category Parent { get; set; }

        [NotMapped]
        public bool IsRootCategory => Parent == null;
    }
}
