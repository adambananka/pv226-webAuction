using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAuction.DataAccessLayer.EntityFramework.Entities
{
    public class Rating
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey(nameof(Author))]
        public Guid AuthorId { get; set; }

        public virtual User Author { get; set; }

        public DateTime Time { get; set; }

        [Range(0, 5)]
        public int Stars { get; set; } //alebo co teda dame?

        public string Text { get; set; }
    }
}
