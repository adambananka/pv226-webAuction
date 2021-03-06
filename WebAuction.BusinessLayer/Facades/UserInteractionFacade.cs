﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.Facades.Common;
using WebAuction.BusinessLayer.Services.Comments;
using WebAuction.BusinessLayer.Services.Ratings;
using WebAuction.BusinessLayer.Services.Users;
using WebAuction.Infrastructure.UnitOfWork;

namespace WebAuction.BusinessLayer.Facades
{
    public class UserInteractionFacade : FacadeBase
    {
        private readonly ICommentService _commentService;
        private readonly IRatingService _ratingService;
        private readonly IUserService _userService;

        public UserInteractionFacade(IUnitOfWorkProvider unitOfWorkProvider, IRatingService ratingService,
            ICommentService commentService, IUserService userService) : base(unitOfWorkProvider)
        {
            _ratingService = ratingService;
            _commentService = commentService;
            _userService = userService;
        }

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

        public async Task<IEnumerable<RatingDto>> GetAllRatingsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await _ratingService.ListAllAsync()).Items;
            }
        }

        public async Task<Guid> CreateRatingAsync(RatingDto rating, string userEmail)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                rating.SellerId = (await _userService.GetUserAccordingToEmailAsync(userEmail)).Id;
                var ratingId = _ratingService.Create(rating);
                await uow.Commit();
                return ratingId;
            }
        }

        public async Task<bool> EditRatingAsync(RatingDto ratingDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var updatedRating = await _ratingService.GetAsync(ratingDto.Id, false);
                if (updatedRating == null)
                {
                    return false;
                }
                await _ratingService.Update(ratingDto);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteRatingAsync(Guid ratingId)
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
