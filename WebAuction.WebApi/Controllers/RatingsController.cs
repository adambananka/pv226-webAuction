using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.Facades;
using WebAuction.WebApi.Models.Ratings;

namespace WebAuction.WebApi.Controllers
{
    public class RatingsController : ApiController
    {
        public UserInteractionFacade InteractionFacade { get; set; }

        public async Task<RatingDto> Get(Guid id)
        {
            var rating = await InteractionFacade.GetRatingAsync(id);
            if (rating == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            SetSafeRating(new[] {rating});

            return rating;
        }

        public async Task<IEnumerable<RatingDto>> Get()
        {
            var ratings = await InteractionFacade.GetAllRatingsAsync();
            SetSafeRating(ratings);

            return ratings;
        }

        public async Task<string> Post([FromBody] RatingCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                throw  new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var ratingId = await InteractionFacade.CreateRatingAsync(model.Rating, model.Email);
            if (ratingId.Equals(Guid.Empty))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return $"Rating was created with id: {ratingId}";
        }

        public async Task<string> Put(Guid id, [FromBody]RatingDto rating)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var success = await InteractionFacade.EditRatingAsync(rating);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Updated rating with id: {id}";
        }


        public async Task<string> Delete(Guid id)
        {
            var success = await InteractionFacade.DeleteRatingAsync(id);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return $"Deleted ratting with id: {id}";
        }

        private void SetSafeRating(IEnumerable<RatingDto> ratings)
        {
            foreach (var rating in ratings)
            {
                rating.Id = Guid.Empty;
                if (rating.Seller != null)
                {
                    rating.Seller.Id = Guid.Empty;
                }
            }
        }
    }
}
