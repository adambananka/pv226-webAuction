﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using WebAuction.BusinessLayer.QueryObjects.Common;
using WebAuction.BusinessLayer.Services.Common;
using WebAuction.Infrastructure;
using WebAuction.Infrastructure.Query;
using WebAuction.DataAccessLayer.EntityFramework.Entities;

namespace WebAuction.BusinessLayer.Services.Ratings
{
    public class RatingService : CrudQueryServiceBase<Rating, RatingDto, RatingFilterDto>, IRatingService
    {
        public RatingService(IMapper mapper, IRepository<Rating> repository,
            QueryObjectBase<RatingDto, Rating, RatingFilterDto, IQuery<Rating>> query) : base(mapper, repository, query) {}

        protected override async Task<Rating> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<IEnumerable<RatingDto>> GetRatingsBySellerId(Guid sellerId)
        {
            var queryResult = await Query.ExecuteQuery(new RatingFilterDto {SellerId = sellerId});
            return queryResult.Items;
        }

        public double GetAverageRatingForSeller(Guid sellerId)
        {
            var ratings = GetRatingsBySellerId(sellerId).Result.ToList();
            return ratings.Sum(r => r.Stars) * 1.0 / ratings.Count;
        }
    }
}
