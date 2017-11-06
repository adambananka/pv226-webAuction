using WebAuction.BusinessLayer.DataTransferObjects.Common;

namespace WebAuction.BusinessLayer.DataTransferObjects.Filters
{
    public class CategoryFilterDto : FilterDtoBase
    {
        public string[] Names { get; set; }
    }
}
