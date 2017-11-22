using System.ComponentModel.DataAnnotations;
using WebAuction.BusinessLayer.DataTransferObjects;

namespace WebAuction.WebApi.Models.Ratings
{
    public class RatingCreateModel
    {
        public RatingDto Rating { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
    }
}