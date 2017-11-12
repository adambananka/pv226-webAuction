using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Common;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;

namespace WebAuction.BusinessLayer.Services.Ratings
{
    public interface IRatingService
    {
        /// <summary>
        /// Gets rating with given sellerId
        /// </summary>
        /// <param name="sellerId">sellerId</param>
        /// <returns>Ratings for user with given Id</returns>
        Task<IEnumerable<RatingDto>> GetRatingsBySellerId(Guid sellerId);

        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<RatingDto> GetAsync(Guid entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(RatingDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(RatingDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(Guid entityId);

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available dtos (for given type)</returns>
        Task<QueryResultDto<RatingDto, RatingFilterDto>> ListAllAsync();

        /// <summary>
        /// Gets average rating for given user
        /// </summary>
        /// <param name="sellerId">user's Id</param>
        /// <returns>average rating of user</returns>
        double GetAverageRatingForSeller(Guid sellerId);
    }
}
