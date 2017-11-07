using System;
using WebAuction.BusinessLayer.DataTransferObjects.Common;

namespace WebAuction.BusinessLayer.DataTransferObjects
{
    public class RatingDto : DtoBase
    {
        public UserDto Seller { get; set; }

        public DateTime Time { get; set; }

        public int Stars { get; set; }

        public string Feedback { get; set; }
    }
}
