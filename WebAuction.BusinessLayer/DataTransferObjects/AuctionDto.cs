using System;
using System.ComponentModel.DataAnnotations;
using WebAuction.BusinessLayer.DataTransferObjects.Common;
using WebAuction.DataAccessLayer.EntityFramework.Validation;

namespace WebAuction.BusinessLayer.DataTransferObjects
{
    public class AuctionDto : DtoBase
    {
        [Required, MaxLength(256)]
        public string Name { get; set; }

        public string Description { get; set; }

        [MaxLength(1024)]
        public string PhotoUri { get; set; }

        public Guid CategoryId { get; set; }

        public CategoryDto Category { get; set; }

        public Guid SellerId { get; set; }
        
        [PositiveDecimal]
        public decimal StartingPrice { get; set; }

        [PositiveDecimal]
        public decimal ActualPrice { get; set; }

        [PositiveDecimal]
        public decimal MinimalBid { get; set; }

        [PositiveDecimal]
        public decimal BuyoutPrice { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public DateTime? SellTime { get; set; }

        public string HandoverOptions { get; set; }

        public bool Ended { get; set; }

        [Range(0, int.MaxValue)]
        public int DisplayCount { get; set; }
    }
}
