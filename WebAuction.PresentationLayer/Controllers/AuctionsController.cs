﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Common;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using WebAuction.BusinessLayer.Facades;
using WebAuction.PresentationLayer.Models.Auctions;
using X.PagedList;

namespace WebAuction.PresentationLayer.Controllers
{
    public class AuctionsController : Controller
    {
        public const int PageSize = 2;

        private const string FilterSessionKey = "filter";
        private const string CategoryTreesSessionKey = "categoryTrees";

        public AuctionProcessFacade AuctionProcessFacade { get; set; }


        public async Task<ActionResult> Index(int page = 1)
        {
            var filter = Session[FilterSessionKey] as AuctionFilterDto ?? new AuctionFilterDto { PageSize = PageSize/*, OnlyActive = true*/};
            filter.RequestedPageNumber = page;
            var result = await AuctionProcessFacade.GetAuctionsAsync(filter);

            var categoryTrees = Session[CategoryTreesSessionKey] as IList<CategoryDto>;
            var model = await InitializeProductListViewModel(result, categoryTrees);
            return View("AuctionListView", model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(AuctionListViewModel model)
        {
            model.Filter.PageSize = PageSize;
            //model.Filter.CategoryIds = ProcessCategoryIds(model);
            Session[FilterSessionKey] = model.Filter;
            Session[CategoryTreesSessionKey] = model.Categories;

            var result = await AuctionProcessFacade.GetAuctionsAsync(model.Filter);
            var newModel = await InitializeProductListViewModel(result, model.Categories);
            return View("AuctionListView", newModel);
        }

        public ActionResult ClearFilter()
        {
            Session[FilterSessionKey] = null;
            Session[CategoryTreesSessionKey] = null;
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var model = await AuctionProcessFacade.GetAuctionAsync(id);
            return View("AuctionDetailView", model);
        }

        

        #region Helper methods

        /// <summary>
        /// Initializes new AuctionListViewModel according to its parameters
        /// </summary>
        /// <param name="result">Auction list query result containing auctions page and related data</param>
        /// <param name="categories">List of category trees</param>
        /// <returns>Initialized instance of AuctionListViewModel</returns>
        private async Task<AuctionListViewModel> InitializeProductListViewModel(QueryResultDto<AuctionDto, AuctionFilterDto> result, IList<CategoryDto> categories = null)
        {
            return new AuctionListViewModel
            {
                Auctions = new StaticPagedList<AuctionDto>(result.Items, result.RequestedPageNumber ?? 1, PageSize, (int)result.TotalItemsCount),
                Categories = categories ?? await AuctionProcessFacade.GetAllCategories() as IList<CategoryDto>,
                Filter = result.Filter
            };
        }

        /// <summary>
        /// Processes category IDs by filtering out unchecked categories
        /// </summary>
        /// <param name="model">model containing category trees</param>
        /// <returns>selected categories</returns>
        private static Guid[] ProcessCategoryIds(AuctionListViewModel model)
        {
            var selectedCategoryIds = new List<Guid>();
            foreach (var categoryTreeRoot in model.Categories)
            {
                if (categoryTreeRoot.IsActive)
                {
                    selectedCategoryIds.Add(categoryTreeRoot.Id);
                    selectedCategoryIds.AddRange(model.Categories
                        .Where(node => node.ParentId == categoryTreeRoot.Id && node.IsActive)
                        .Select(node => node.Id));
                }
            }
            return selectedCategoryIds.ToArray();
        }

        #endregion
    }
}