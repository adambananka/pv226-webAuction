﻿using System;
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
    public class BidQueryObject : QueryObjectBase<BidDto, Bid, BidFilterDto, IQuery<Bid>>
    {
        public BidQueryObject(IMapper mapper, IQuery<Bid> query) : base(mapper, query)
        {
        }

        protected override IQuery<Bid> ApplyWhereClause(IQuery<Bid> query, BidFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();
            AddIfDefined(FilterAuction(filter), definedPredicates);
            AddIfDefined(FilterBuyer(filter), definedPredicates);

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

        private static SimplePredicate FilterAuction(BidFilterDto filter)
        {
            return filter.AuctionId.Equals(Guid.Empty)
                ? null
                : new SimplePredicate(nameof(Bid.AuctionId), ValueComparingOperator.Equal, filter.AuctionId);
        }

        private static SimplePredicate FilterBuyer(BidFilterDto filter)
        {
            return filter.BuyerId.Equals(Guid.Empty)
                ? null
                : new SimplePredicate(nameof(Bid.BuyerId), ValueComparingOperator.Equal, filter.BuyerId);
        }
    }
}