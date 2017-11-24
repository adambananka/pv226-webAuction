using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAuction.BusinessLayer.Facades;

namespace WebAuction.PresentationLayer.Controllers
{
    public class AuctionsController : Controller
    {
        public AuctionProcessFacade AuctionProcessFacade { get; set; }

        public async Task<ActionResult> Details(Guid id)
        {
            var model = await AuctionProcessFacade.GetAuctionAsync(id);
            throw new NotImplementedException("trololo");
        }
    }
}