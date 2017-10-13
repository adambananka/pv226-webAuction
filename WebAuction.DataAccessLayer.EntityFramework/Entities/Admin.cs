using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAuction.DataAccessLayer.EntityFramework.Entities
{
    public class Admin
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [MaxLength(256)]
        public string Name { get; set; } //treba davat vsade/niekde tag Required ?

        public string Login { get; set; } //nemali sme, treba?

        public string Password { get; set; } //nemali sme, treba?
    }
}
