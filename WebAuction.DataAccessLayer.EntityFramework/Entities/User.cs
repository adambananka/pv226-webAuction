using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAuction.Infrastructure;

namespace WebAuction.DataAccessLayer.EntityFramework.Entities
{
    public class User : UserLogin
    {
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

        public DateTime BirthDate { get; set; } = new DateTime(1, 1, 1);
    }
}
