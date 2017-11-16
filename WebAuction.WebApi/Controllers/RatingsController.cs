using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.Facades;

namespace WebAuction.WebApi.Controllers
{
    public class RatingsController : ApiController
    {
        public UserInteractionFacade InteractionFacade { get; set; }

        public async Task<RatingDto> Get(Guid id)
        {
            return new RatingDto()
            {
                Feedback = InteractionFacade == null ? "null" : "not"
            };

            Console.WriteLine("lol");
            var rating = await InteractionFacade.GetRatingAsync(id);
//            if (rating == null)
//            {
//                throw new HttpResponseException(HttpStatusCode.NotFound);
//            }
//            rating.Id = Guid.Empty;

            return rating;
        }   
    }
}
