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

        
    }
}