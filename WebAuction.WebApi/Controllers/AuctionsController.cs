using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using WebAuction.BusinessLayer.Facades;
using WebAuction.WebApi.Models.Auctions;

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
            var auction = await AuctionProcessFacade.GetAuctionAsync(id);
            if (auction == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            SafeSetEntityId(new[] {auction});

            return auction;
        }

        public async Task<IEnumerable<AuctionDto>> Get()
        {
            var auctions = await AuctionProcessFacade.GetAllAuctionsAsync();

            SafeSetEntityId(auctions);

            return auctions;
        }

        public async Task<string> Post([FromBody] AuctionCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var auctionId =
                await AuctionProcessFacade.CreateAuctionWithCategoryNameForUserAsync(model.Auction, model.UserEmail, model.CategoryName);
            if (auctionId.Equals(Guid.Empty))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return $"Auction was created with id: {auctionId}";
        }

        public async Task<string> Put(Guid id, [FromBody]AuctionDto auction)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var success = await AuctionProcessFacade.EditAuction(auction);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Updated auction with id: {id}";
        }

        public async Task<string> Delete(Guid id)
        {
            var success = await AuctionProcessFacade.DeleteAuction(id);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return $"Deleted auction with id: {id}";
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
