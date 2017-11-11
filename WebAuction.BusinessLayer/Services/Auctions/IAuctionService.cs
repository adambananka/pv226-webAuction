using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Common;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;

namespace WebAuction.BusinessLayer.Services.Auctions
{
    public interface IAuctionService
    {
        /// <summary>
        /// Gets auctions with given seller
        /// </summary>
        /// <param name="sellerId">seller's id</param>
        /// <returns>Auctions with given seller</returns>
        Task<IEnumerable<AuctionDto>> GetAuctionsAccordingToSellerAsync(Guid sellerId);

        /// <summary>
        /// Gets auctions within given categories
        /// </summary>
        /// <param name="categoryIds">ids of categories</param>
        /// <returns>Auctions within given categories</returns>
        Task<IEnumerable<AuctionDto>> GetAuctionsAccordingToCategoriesAsync(Guid[] categoryIds);

        /// <summary>
        /// Gets auctions with given name
        /// </summary>
        /// <param name="name">auction's name</param>
        /// <returns>Auctions with given name</returns>
        Task<IEnumerable<AuctionDto>> GetAuctionsAccordingToNameAsync(string name);

        /// <summary>
        /// Gets auctions with given parameters
        /// Null should be used to ignore name/sellerId/categoryIds
        /// </summary>
        /// <param name="name">auction's name</param>
        /// <param name="sellerId">seller's id</param>
        /// <param name="categoryIds">ids of categories</param>
        /// <param name="maximalPrice">maximal actual price of auction item</param>
        /// <returns>Auctions with given parameters</returns>
        Task<IEnumerable<AuctionDto>> GetAuctionsAccordingToFilterAsync(string name, Guid sellerId, Guid[] categoryIds, decimal maximalPrice = decimal.MaxValue);

        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<AuctionDto> GetAsync(Guid entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(AuctionDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(AuctionDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void DeleteProduct(Guid entityId);

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available dtos (for given type)</returns>
        Task<QueryResultDto<AuctionDto, AuctionFilterDto>> ListAllAsync();
    }
}
