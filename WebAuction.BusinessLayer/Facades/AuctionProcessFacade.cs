using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Common;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using WebAuction.BusinessLayer.Facades.Common;
using WebAuction.BusinessLayer.Services.Auctions;
using WebAuction.BusinessLayer.Services.Bids;
using WebAuction.BusinessLayer.Services.Categories;
using WebAuction.BusinessLayer.Services.ClosingAuction;
using WebAuction.BusinessLayer.Services.UserLogins;
using WebAuction.Infrastructure.UnitOfWork;

namespace WebAuction.BusinessLayer.Facades
{
    public class AuctionProcessFacade : FacadeBase
    {
        private readonly IAuctionService _auctionService;
        private readonly IClosingAuctionService _closingAuctionService;
        private readonly IBidService _bidService;
        private readonly ICategoryService _categoryService;
        private readonly IUserLoginService _userLoginService;

        public AuctionProcessFacade(IUnitOfWorkProvider unitOfWorkProvider, IAuctionService auctionService,
            IClosingAuctionService closingAuctionService, IBidService bidService, ICategoryService categoryService, IUserLoginService userLoginService)
            : base(unitOfWorkProvider)
        {
            _auctionService = auctionService;
            _closingAuctionService = closingAuctionService;
            _bidService = bidService;
            _categoryService = categoryService;
            _userLoginService = userLoginService;
        }

        #region AuctionCRUD

        public async Task<AuctionDto> GetAuctionAsync(Guid auctionId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _auctionService.GetAsync(auctionId);
            }
        }   

        public async Task<QueryResultDto<AuctionDto, AuctionFilterDto>> GetAuctionsAsync(AuctionFilterDto filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _auctionService.ListAuctionsAsync(filter);
            }
        }

        #region GetAuctionAccordingTo...

        public async Task<IEnumerable<AuctionDto>> GetAuctionsAccordingToNameAsync(string name)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _auctionService.GetAuctionsAccordingToNameAsync(name);
            }
        }

        public async Task<IEnumerable<AuctionDto>> GetActiveAuctionsAccordingToNameAsync(string name)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _auctionService.GetActiveAuctionsAccordingToNameAsync(name);
            }
        }

        public async Task<IEnumerable<AuctionDto>> GetAuctionsAccordingToCategoriesAsync(Guid[] categoryIds)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _auctionService.GetAuctionsAccordingToCategoriesAsync(categoryIds);
            }
        }

        public async Task<IEnumerable<AuctionDto>> GetActiveAuctionsAccordingToCategoriesAsync(Guid[] categoryIds)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _auctionService.GetActiveAuctionsAccordingToCategoriesAsync(categoryIds);
            }
        }

        #endregion

        public async Task<IEnumerable<AuctionDto>> GetAllAuctionsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await _auctionService.ListAllAsync()).Items;
            }
        }

        public async Task<Guid> CreateAuctionWithCategoryNameForUserAsync(AuctionDto auction, string userLogin, string categoryName)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                auction.CategoryId = (await _categoryService.GetCategoriesIdsAccordingToNameAsync(new[] {categoryName})).FirstOrDefault();
                auction.SellerId = (await _userLoginService.GetUserAccordingToUsernameAsync(userLogin)).Id;
                var auctionId = _auctionService.Create(auction);
                await uow.Commit();
                var delay = auction.EndTime.Subtract(DateTime.Now);
                BackgroundJob.Schedule(() => CloseAuctionDueToTimeout(auction), delay);
                return auctionId;
            }
        }

        public async Task<bool> EditAuction(AuctionDto auction)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await _auctionService.GetAsync(auction.Id, false) == null)
                {
                    return false;
                }
                await _auctionService.Update(auction);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteAuction(Guid auctionId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await _auctionService.GetAsync(auctionId, false) == null)
                {
                    return false;
                }

                _auctionService.Delete(auctionId);
                await uow.Commit();
                return true;
            }
        }

        #endregion

        #region BidOperations

        public async Task<IEnumerable<BidDto>> GetBidsToAuctionAsync(Guid auctionId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _bidService.GetBidsAccordingToAuctionAsync(auctionId);
            }
        }

        public async Task MakeBidToAuction(BidDto bid)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                await _bidService.PlaceBid(bid);
                await uow.Commit();
            }
        }

        #endregion

        #region ClosingAuction

        public async Task BuyoutAuction(AuctionDto auction, BidDto bid)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                await _bidService.PlaceBid(bid);
                await uow.Commit();

                var auctionBids = await _bidService.GetBidsAccordingToAuctionAsync(auction.Id);
                _closingAuctionService.CloseAuctionDueToBuyout(auction, auctionBids);
                await uow.Commit();
            }
        }

        public async void CloseAuctionDueToTimeout(AuctionDto auction)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var auctionBids = await _bidService.GetBidsAccordingToAuctionAsync(auction.Id);
                _closingAuctionService.CloseAuctionDueToTimeout(auction, auctionBids);
                await uow.Commit();
            }
        }

        #endregion

        #region CategoriesManagement

        public async Task<Guid> CreateCategoryWithinParentCategoryNameAsync(CategoryDto category, string parentCategory = null)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (parentCategory != null)
                {
                    category.ParentId = (await _categoryService.GetCategoriesIdsAccordingToNameAsync(new[] {parentCategory})).FirstOrDefault();
                }
                var categoryId = _categoryService.Create(category);
                await uow.Commit();

                return categoryId;
            }
        }

        public async Task<bool> EditCategory(CategoryDto category)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await _categoryService.GetAsync(category.Id, false) == null)
                {
                    return false;
                }
                await _categoryService.Update(category);
                await uow.Commit();
                return true;
            }
        }

        public async Task<CategoryDto> GetCategoryAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _categoryService.GetAsync(id);
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await _categoryService.ListAllAsync()).Items;
            }
        }

        public async Task<Guid[]> GetCategoryIdsByNameAsync(params string[] names)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _categoryService.GetCategoriesIdsAccordingToNameAsync(names);
            }
        }

        #endregion
    }
}
