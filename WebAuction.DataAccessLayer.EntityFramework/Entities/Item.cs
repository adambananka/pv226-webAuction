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

        [MaxLength(256)]
        public string Name { get; set; }

        [ForeignKey(nameof(Seller))]
        public Guid Sellerid { get; set; }

        public virtual User Seller { get; set; }

        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string Description { get; set; }

        public Collection<byte[]> Photos { get; set; } //alebo ako inak?
    }
}
