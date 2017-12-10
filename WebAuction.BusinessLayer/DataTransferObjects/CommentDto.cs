using System;
using WebAuction.BusinessLayer.DataTransferObjects.Common;

namespace WebAuction.BusinessLayer.DataTransferObjects
{
    public class CommentDto : DtoBase
    {
        public Guid AuctionId { get; set; }

        public Guid UserId { get; set; }

        public string Username { get; set; }

        public DateTime Time { get; set; }

        public string Text { get; set; }
    }
}
