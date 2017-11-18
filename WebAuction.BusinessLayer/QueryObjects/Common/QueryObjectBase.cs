using System.Threading.Tasks;
using AutoMapper;
using WebAuction.BusinessLayer.DataTransferObjects.Common;
using WebAuction.Infrastructure;
using WebAuction.Infrastructure.Query;

namespace WebAuction.BusinessLayer.QueryObjects.Common
{
    // todo - pridat konvecne where clause dle filtra
    // 	MinimalPrice => new SimplePredicate(nameof(MinimalPrice), GreaterThanOrEqual,  ..)
    public abstract class QueryObjectBase<TDto, TEntity, TFilter, TQuery>
        where TFilter : FilterDtoBase
        where TQuery : IQuery<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly IMapper _mapper;

        protected readonly IQuery<TEntity> Query;

        protected QueryObjectBase(IMapper mapper, TQuery query)
        {
            _mapper = mapper;
            Query = query;
        }

        protected abstract IQuery<TEntity> ApplyWhereClause(IQuery<TEntity> query, TFilter filter);

        public virtual async Task<QueryResultDto<TDto, TFilter>> ExecuteQuery(TFilter filter)
        {
            var query = ApplyWhereClause(Query, filter);
            if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
            {
                query = query.SortBy(filter.SortCriteria, filter.SortAscending);
            }
            if (filter.RequestedPageNumber.HasValue)
            {
                query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }
            var queryResult = await query.ExecuteAsync();

            var queryResultDto = _mapper.Map<QueryResultDto<TDto, TFilter>>(queryResult);
            queryResultDto.Filter = filter;
            return queryResultDto;
        }
    }
}