using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.Facades;

namespace WebAuction.WebApi.Controllers
{
    public class AuctionsController : ApiController
    {
        public AuctionProcessFacade AuctionProcessFacade { get; set; }

        public async Task<AuctionDto> Get(Guid id)
        {
            return await AuctionProcessFacade.GetAuctionAsync(id);
        }

        public async Task<IEnumerable<AuctionDto>> Get()
        {
            var auctions = await AuctionProcessFacade.GetAllAuctionsAsync();

            foreach (var auctionDto in auctions)
            {
                auctionDto.Id = Guid.Empty;
            }

            return auctions;
        }
    }
}
