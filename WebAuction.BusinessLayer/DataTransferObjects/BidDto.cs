﻿using System;
using WebAuction.BusinessLayer.DataTransferObjects.Common;

namespace WebAuction.BusinessLayer.DataTransferObjects
{
    public class BidDto : DtoBase
    {
        public AuctionDto Auction { get; set; }

        public Guid BuyerId { get; set; }

        public DateTime Time { get; set; }

        public decimal BidAmount { get; set; }

        public decimal NewItemPrice { get; set; }
    }
}
