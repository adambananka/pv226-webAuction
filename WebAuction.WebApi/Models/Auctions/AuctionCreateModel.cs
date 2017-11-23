using System.ComponentModel.DataAnnotations;
using WebAuction.BusinessLayer.DataTransferObjects;

namespace WebAuction.WebApi.Models.Auctions
{
    public class AuctionCreateModel
    {
        public AuctionDto Auction { get; set; }

        [Required, MinLength(2), MaxLength(256)]
        public string CategoryName { get; set; }

        [Required, EmailAddress]
        public string UserEmail { get; set; }
    }
}