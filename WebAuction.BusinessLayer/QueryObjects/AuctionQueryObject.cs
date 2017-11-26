using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using WebAuction.BusinessLayer.QueryObjects.Common;
using WebAuction.DataAccessLayer.EntityFramework.Entities;
using WebAuction.Infrastructure.Query;
using WebAuction.Infrastructure.Query.Predicates;
using WebAuction.Infrastructure.Query.Predicates.Operators;

namespace WebAuction.BusinessLayer.QueryObjects
{
    public class AuctionQueryObject : QueryObjectBase<AuctionDto, Auction, AuctionFilterDto, IQuery<Auction>>
    {
        public AuctionQueryObject(IMapper mapper, IQuery<Auction> query) : base(mapper, query) {}

        protected override IQuery<Auction> ApplyWhereClause(IQuery<Auction> query, AuctionFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();
            AddIfDefined(FilterItemName(filter), definedPredicates);
            AddIfDefined(FilterSeller(filter), definedPredicates);
            AddIfDefined(FilterCategories(filter), definedPredicates);
            AddIfDefined(FilterOnlyActive(filter), definedPredicates);
            AddIfDefined(FilterMaximalPrice(filter), definedPredicates);
            AddIfDefined(FilterMinimalPrice(filter), definedPredicates);
            AddIfDefined(FilterMinimalEndTime(filter), definedPredicates);
            AddIfDefined(FilterMaximalEndTime(filter), definedPredicates);

            if (definedPredicates.Count == 0)
            {
                return query;
            }
            if (definedPredicates.Count == 1)
            {
                return query.Where(definedPredicates.First());
            }
            var resultPredicate = new CompositePredicate(definedPredicates);
            return query.Where(resultPredicate);
        }

        private static void AddIfDefined(IPredicate predicate, ICollection<IPredicate> definedPredicates)
        {
            if (predicate != null)
            {
                definedPredicates.Add(predicate);
            }
        }

        private static SimplePredicate FilterItemName(AuctionFilterDto filter)
        {
            return string.IsNullOrWhiteSpace(filter.SearchedName)
                ? null
                : new SimplePredicate(nameof(Auction.Name), ValueComparingOperator.StringContains, filter.SearchedName);
        }

        private static SimplePredicate FilterSeller(AuctionFilterDto filter)
        {
            return filter.SellerId.Equals(Guid.Empty)
                ? null
                : new SimplePredicate(nameof(Auction.SellerId), ValueComparingOperator.Equal, filter.SellerId);
        }

        private static CompositePredicate FilterCategories(AuctionFilterDto filter)
        {
            if (filter.CategoryIds == null || filter.CategoryIds.Any())
            {
                return null;
            }
            var predicates = new List<IPredicate>(filter.CategoryIds
                .Select(categoryId =>
                    new SimplePredicate(nameof(Auction.CategoryId), ValueComparingOperator.Equal, categoryId)));
            return new CompositePredicate(predicates, LogicalOperator.OR);
        }

        private static SimplePredicate FilterOnlyActive(AuctionFilterDto filter)
        {
            return !filter.OnlyActive 
                ? null 
                : new SimplePredicate(nameof(Auction.Ended), ValueComparingOperator.Equal, false);
        }

        private static SimplePredicate FilterMaximalPrice(AuctionFilterDto filter)
        {
            if (filter.MaximalActualPrice == decimal.MaxValue)
            {
                return null;
            }

            return new SimplePredicate(nameof(Auction.ActualPrice), ValueComparingOperator.LessThanOrEqual,
                filter.MaximalActualPrice);
        }

        private static SimplePredicate FilterMinimalPrice(AuctionFilterDto filter)
        {
            if (filter.MinimalActualPrice == decimal.MinValue)
            {
                return null;
            }

            return new SimplePredicate(nameof(Auction.ActualPrice), ValueComparingOperator.GreaterThanOrEqual,
                filter.MinimalActualPrice);
        }

        private static SimplePredicate FilterMinimalEndTime(AuctionFilterDto filter)
        {
            if (filter.MinimalEndTime == DateTime.MinValue)
            {
                return null;
            }

            return new SimplePredicate(nameof(Auction.EndTime), ValueComparingOperator.GreaterThanOrEqual,
                filter.MinimalEndTime);
        }

        private static SimplePredicate FilterMaximalEndTime(AuctionFilterDto filter)
        {
            if (filter.MaximalEndTime == DateTime.MaxValue)
            {
                return null;
            }

            return new SimplePredicate(nameof(Auction.EndTime), ValueComparingOperator.LessThanOrEqual,
                filter.MaximalEndTime);
        }
    }
}
