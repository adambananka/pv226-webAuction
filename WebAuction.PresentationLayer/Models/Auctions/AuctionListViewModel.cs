using System.Collections.Generic;
using System.Web.Mvc;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using X.PagedList;

namespace WebAuction.PresentationLayer.Models.Auctions
{
    public class AuctionListViewModel
    {
        public string[] AuctionSortCriteria => new[] { nameof(AuctionDto.Name), nameof(AuctionDto.ActualPrice), nameof(AuctionDto.EndTime) };

        public IList<CategoryDto> Categories { get; set; }

        public IPagedList<AuctionDto> Auctions { get; set; }

        public AuctionFilterDto Filter { get; set; }

        public SelectList AllSortCriteria => new SelectList(AuctionSortCriteria);
    }
}