using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAuction.Infrastructure;

namespace WebAuction.DataAccessLayer.EntityFramework.Entities
{
    public class User : IEntity
    {
        [NotMapped]
        public string TableName { get; set; } = nameof(WebAuctionDbContext.Users);

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        public bool IsAdmin { get; set; }

        [Required, StringLength(64)]
        public string Login { get; set; }

        [Required, StringLength(64)]
        public string PasswordHash { get; set; }

        [Required, StringLength(64)]
        public string PasswordSal { get; set; }
    }
}
