﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAuction.DataAccessLayer.EntityFramework.Entities
{
    public class Bid
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Buyer))]
        public Guid BuyerId { get; set; }

        public virtual User Buyer { get; set; }

        public DateTime Time { get; set; }

        [Range(typeof(decimal), "0", "999999999999")]
        public decimal BidAmount { get; set; }

        [Range(typeof(decimal), "0", "999999999999")]
        public decimal NewItemPrice { get; set; }
    }
}
