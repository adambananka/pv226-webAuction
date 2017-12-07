using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using WebAuction.BusinessLayer.Facades;
using X.PagedList;

namespace WebAuction.PresentationLayer.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminAuctionsController : Controller
    {
        public AuctionProcessFacade AuctionFacade { get; set; }

        public async Task<ActionResult> Index(int page = 1)
        {
            var result = await AuctionFacade.GetAuctionsAsync(new AuctionFilterDto());
            var model = new StaticPagedList<AuctionDto>(result.Items, page, result.PageSize,
                (int) result.TotalItemsCount);

            return View(model);
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            await AuctionFacade.DeleteAuction(id);
            return RedirectToAction("Index");
        }
    }
}