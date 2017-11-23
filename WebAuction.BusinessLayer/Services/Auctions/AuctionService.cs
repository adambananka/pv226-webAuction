using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Common;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using WebAuction.BusinessLayer.QueryObjects.Common;
using WebAuction.BusinessLayer.Services.Common;
using WebAuction.DataAccessLayer.EntityFramework.Entities;
using WebAuction.Infrastructure;
using WebAuction.Infrastructure.Query;

namespace WebAuction.BusinessLayer.Services.Auctions
{
    public class AuctionService : CrudQueryServiceBase<Auction, AuctionDto, AuctionFilterDto>, IAuctionService
    {
        public AuctionService(IMapper mapper, IRepository<Auction> repository,
            QueryObjectBase<AuctionDto, Auction, AuctionFilterDto, IQuery<Auction>> query) : base(mapper, repository,
            query)
        {
        }

        protected override async Task<Auction> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Auction.Category));
        }

        public async Task<IEnumerable<AuctionDto>> GetAuctionsAccordingToSellerAsync(Guid sellerId)
        {
            var queryResult = await Query.ExecuteQuery(new AuctionFilterDto {SellerId = sellerId});
            return queryResult.Items;
        }

        public async Task<IEnumerable<AuctionDto>> GetAuctionsAccordingToCategoriesAsync(Guid[] categoryIds)
        {
            var queryResult = await Query.ExecuteQuery(new AuctionFilterDto {CategoryIds = categoryIds});
            return queryResult.Items;
        }

        public async Task<IEnumerable<AuctionDto>> GetActiveAuctionsAccordingToCategoriesAsync(Guid[] categoryIds)
        {
            var queryResult = await Query.ExecuteQuery(new AuctionFilterDto { CategoryIds = categoryIds, OnlyActive = true});
            return queryResult.Items;
        }

        public async Task<IEnumerable<AuctionDto>> GetAuctionsAccordingToNameAsync(string name)
        {
            var queryResult = await Query.ExecuteQuery(new AuctionFilterDto {SearchedName = name});
            return queryResult.Items;
        }

        public async Task<IEnumerable<AuctionDto>> GetActiveAuctionsAccordingToNameAsync(string name)
        {
            var queryResult = await Query.ExecuteQuery(new AuctionFilterDto { SearchedName = name, OnlyActive = true});
            return queryResult.Items;
        }

        public Task<QueryResultDto<AuctionDto, AuctionFilterDto>> ListAuctionsAsync(AuctionFilterDto filter)
        {
            return Query.ExecuteQuery(filter);
        }
    }
}