using System;
using WebAuction.BusinessLayer.DataTransferObjects.Common;

namespace WebAuction.BusinessLayer.DataTransferObjects.Filters
{
    public class CommentFilterDto : FilterDtoBase
    {
        public Guid AuctionId { get; set; }

        public Guid UserId { get; set; }
    }
}
