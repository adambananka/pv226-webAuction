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
    public class CategoryQueryObject : QueryObjectBase<CategoryDto, Category, CategoryFilterDto, IQuery<Category>>
    {
        public CategoryQueryObject(IMapper mapper, IQuery<Category> query) : base(mapper, query) { }

        protected override IQuery<Category> ApplyWhereClause(IQuery<Category> query, CategoryFilterDto filter)
        {
            if (filter.Names == null || !filter.Names.Any())
            {
                return query;
            }
            var categoryNamePredicates = new List<IPredicate>(filter.Names
                .Select(name => new SimplePredicate(
                    nameof(Category.Name),
                    ValueComparingOperator.Equal,
                    name)));
            var predicate = new CompositePredicate(categoryNamePredicates, LogicalOperator.OR);

            return query.Where(predicate);
        }
    }
}
