﻿using WebAuction.BusinessLayer.DataTransferObjects.Common;

namespace WebAuction.BusinessLayer.DataTransferObjects.Filters
{
    public class UserFilterDto : FilterDtoBase
    {
        public string Email { get; set; }
    }
}
