using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Common;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;

namespace WebAuction.BusinessLayer.Services.Bids
{
    public interface IBidService
    {
        /// <summary>
        /// Gets bids to given auction
        /// </summary>
        /// <param name="auctionId">auction's id</param>
        /// <returns>Bids to given auction</returns>
        Task<IEnumerable<BidDto>> GetBidsAccordingToAuctionAsync(Guid auctionId);

        /// <summary>
        /// Gets bids of given user
        /// </summary>
        /// <param name="buyerId">user's id</param>
        /// <returns>Bids of given user</returns>
        Task<IEnumerable<BidDto>> GetBidsAccordingToBuyerAsync(Guid buyerId);

        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<BidDto> GetAsync(Guid entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(BidDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(BidDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(Guid entityId);

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available dtos (for given type)</returns>
        Task<QueryResultDto<BidDto, BidFilterDto>> ListAllAsync();

        Task PlaceBid(BidDto bidDto);
    }
}
