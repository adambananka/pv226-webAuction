using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using WebAuction.BusinessLayer.QueryObjects.Common;
using WebAuction.BusinessLayer.Services.Common;
using WebAuction.DataAccessLayer.EntityFramework.Entities;
using WebAuction.Infrastructure;
using WebAuction.Infrastructure.Query;

namespace WebAuction.BusinessLayer.Services.Comments
{
    public class CommentService : CrudQueryServiceBase<Comment, CommentDto, CommentFilterDto>, ICommentService
    {
        public CommentService(IMapper mapper, IRepository<Comment> repository,
            QueryObjectBase<CommentDto, Comment, CommentFilterDto, IQuery<Comment>> query)
            : base(mapper, repository, query) {}

        protected override Task<Comment> GetWithIncludesAsync(Guid entityId)
        {
            return Repository.GetAsync(entityId);
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByUserId(Guid userId)
        {
            var queryResult = await Query.ExecuteQuery(new CommentFilterDto {UserId = userId});
            return queryResult.Items;
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByAuctionId(Guid auctionId)
        {
            var queryResult = await Query.ExecuteQuery(new CommentFilterDto {AuctionId = auctionId});
            return queryResult.Items;
        }
    }
}
