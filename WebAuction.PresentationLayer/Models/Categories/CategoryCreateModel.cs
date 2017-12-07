using WebAuction.BusinessLayer.DataTransferObjects;

namespace WebAuction.PresentationLayer.Models.Categories
{
    public class CategoryCreateModel
    {
        public CategoryDto Category { get; set; }
        public string ParentCategory { get; set; }
    }
}