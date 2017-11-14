using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.Facades.Common;
using WebAuction.BusinessLayer.Services.Auctions;
using WebAuction.BusinessLayer.Services.Bids;
using WebAuction.BusinessLayer.Services.Comments;
using WebAuction.BusinessLayer.Services.Ratings;
using WebAuction.BusinessLayer.Services.Users;
using WebAuction.Infrastructure.UnitOfWork;

namespace WebAuction.BusinessLayer.Facades
{
    public class UserInteractionFacade : FacadeBase
    {
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        private readonly IRatingService _ratingService;
        private readonly IAuctionService _auctionService;
        private readonly IBidService _bidService;

        public UserInteractionFacade(IUnitOfWorkProvider unitOfWorkProvider, IBidService bidService,
            IAuctionService auctionService, IRatingService ratingService, ICommentService commentService,
            IUserService userService) : base(unitOfWorkProvider)
        {
            _bidService = bidService;
            _auctionService = auctionService;
            _ratingService = ratingService;
            _commentService = commentService;
            _userService = userService;
        }

        // spracovanie rating a comment todo 

        #region CommentOperations

        public async Task<CommentDto> GetCommentAsync(Guid commentId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _commentService.GetAsync(commentId);
            }
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsAccordingToAuction(Guid auctionId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _commentService.GetCommentsByAuctionId(auctionId);
            }
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsAccordingToUser(Guid userId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _commentService.GetCommentsByUserId(userId);
            }
        }

        public async Task<Guid> CreateComment(CommentDto comment)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var commentId = _commentService.Create(comment);
                await uow.Commit();
                return commentId;
            }
        }

        public async Task<bool> EditComment(CommentDto comment)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await _commentService.GetAsync(comment.Id, false) == null)
                {
                    return false;
                }
                await _commentService.Update(comment);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteComment(Guid commentId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await _commentService.GetAsync(commentId, false) == null)
                {
                    return false;
                }
                _commentService.Delete(commentId);
                await uow.Commit();
                return true;
            }
        }

        #endregion

        #region RatingOperations

        public async Task<RatingDto> GetRatingAsync(Guid ratingId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _ratingService.GetAsync(ratingId);
            }
        }

        public async Task<IEnumerable<RatingDto>> GetRatingsAccordingToUser(Guid userId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _ratingService.GetRatingsBySellerId(userId);
            }
        }

        public async Task<Guid> CreateRating(RatingDto rating)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var ratingId = _ratingService.Create(rating);
                await uow.Commit();
                return ratingId;
            }
        }

        public async Task<bool> EditRating(RatingDto rating)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await _ratingService.GetAsync(rating.Id, false) == null)
                {
                    return false;
                }
                await _ratingService.Update(rating);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteRating(Guid ratingId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await _ratingService.GetAsync(ratingId, false) == null)
                {
                    return false;
                }
                _ratingService.Delete(ratingId);
                await uow.Commit();
                return true;
            }
        }

        public double GetAverageRatingForUser(Guid userId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return _ratingService.GetAverageRatingForSeller(userId);
            }
        }

        #endregion
    }
}