using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using WebAuction.BusinessLayer.Facades;

namespace WebAuction.WebApi.Controllers
{
    public class AuctionsController : ApiController
    {
        public AuctionProcessFacade AuctionProcessFacade { get; set; }

        [HttpGet, Route("api/Auctions/Query")]
        public async Task<IEnumerable<AuctionDto>> Query(string sort = null, bool asc = true, string name = null,
            decimal minimalActualPrice = 0m, decimal maximalActualtPrice = decimal.MaxValue,
            DateTime? minimalEndTime = null, DateTime? maximalEndTime = null, [FromUri] string[] category = null)
        {
            if (minimalEndTime == null)
            {
                minimalEndTime = DateTime.MinValue;
            }
            if (maximalEndTime == null)
            {
                maximalEndTime = DateTime.MaxValue;
            }

            var filter = new AuctionFilterDto
            {
                SortCriteria = sort,
                SortAscending = asc,
                SearchedName = name,
                MinimalEndTime = minimalEndTime.Value,
                MaximalEndTime = maximalEndTime.Value,
                MaximalActualPrice = maximalActualtPrice,
                MinimalActualPrice = minimalActualPrice
            };

            var auctions = (await AuctionProcessFacade.GetAuctionsAsync(filter)).Items;

            SafeSetEntityId(auctions);

            return auctions;
        }

        public async Task<AuctionDto> Get(Guid id)
        {
            return await AuctionProcessFacade.GetAuctionAsync(id);
        }

        public async Task<IEnumerable<AuctionDto>> Get()
        {
            var auctions = await AuctionProcessFacade.GetAllAuctionsAsync();

            SafeSetEntityId(auctions);

            return auctions;
        }

        private static void SafeSetEntityId(IEnumerable<AuctionDto> auctions)
        {
            foreach (var auctionDto in auctions)
            {
                auctionDto.Id = Guid.Empty;
            }
        }
    }
}
