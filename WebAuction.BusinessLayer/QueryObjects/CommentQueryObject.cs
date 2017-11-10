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
    public class CommentQueryObject : QueryObjectBase<CommentDto, Comment, CommentFilterDto, IQuery<Comment>>
    {
        public CommentQueryObject(IMapper mapper, IQuery<Comment> query) : base(mapper, query)
        {
        }

        protected override IQuery<Comment> ApplyWhereClause(IQuery<Comment> query, CommentFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();
            AddIfDefined(FilterAuction(filter), definedPredicates);
            AddIfDefined(FilterUser(filter), definedPredicates);

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

        private static SimplePredicate FilterAuction(CommentFilterDto filter)
        {
            return filter.AuctionId.Equals(Guid.Empty)
                ? null
                : new SimplePredicate(nameof(Comment.AuctionId), ValueComparingOperator.Equal, filter.AuctionId);
        }

        private static SimplePredicate FilterUser(CommentFilterDto filter)
        {
            return filter.UserId.Equals(Guid.Empty)
                ? null
                : new SimplePredicate(nameof(Comment.UserId), ValueComparingOperator.Equal, filter.UserId);
        }
    }
}
