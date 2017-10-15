using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAuction.DataAccessLayer.EntityFramework.Entities
{
    public class Auction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Range(typeof(decimal), "0", "999999999999")]
        public decimal StartingPrice { get; set; }

        [Range(typeof(decimal), "0", "999999999999")]
        public decimal ActualPrice { get; set; }

        [Range(typeof(decimal), "0", "999999999999")]
        public decimal MinimalBid { get; set; }

        [Range(typeof(decimal), "0", "999999999999")]
        public decimal BuyoutPrice { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public DateTime? SellTime { get; set; }

        public string HandoverOptions { get; set; }

        public bool Sold { get; set; }

        [Range(0, int.MaxValue)]
        public int DisplayCount { get; set; }
    }
}
