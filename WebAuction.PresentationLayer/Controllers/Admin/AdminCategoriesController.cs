using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.Facades;
using WebAuction.PresentationLayer.Helpers;
using WebAuction.PresentationLayer.Models.Categories;
using X.PagedList;

namespace WebAuction.PresentationLayer.Controllers.Admin
{
    [Authorize(Roles = AuthorizationRoles.Admin)]
    public class AdminCategoriesController : Controller
    {
        public AuctionProcessFacade AuctionFacade { get; set; }

        public async Task<ActionResult> Index(int page = 1)
        {
            var result = (await AuctionFacade.GetAllCategoriesAsync()).ToArray();
            var pageSize = result.Length > 0 ? result.Length : 1;
            var model = new StaticPagedList<CategoryDto>(result, page, pageSize, result.Length);

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CategoryCreateModel model)
        {
            try
            {
                await AuctionFacade.CreateCategoryWithinParentCategoryNameAsync(model.Category, model.ParentCategory);
                return RedirectToAction("Index");
            } catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            var category = await AuctionFacade.GetCategoryAsync(id);

            var model = new CategoryCreateModel
            {
                Category = category,
                ParentCategory = category.Parent == null ? "" : category.Parent.Name
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, CategoryCreateModel model)
        {
            try
            {
                if (model.ParentCategory != null)
                {
                    model.Category.ParentId =
                        (await AuctionFacade.GetCategoryIdsByNameAsync(model.ParentCategory)).SingleOrDefault();
                }
                await AuctionFacade.EditCategory(model.Category);
                return RedirectToAction("Index");
            } catch
            {
                return View();
            }
        }
    }
}
