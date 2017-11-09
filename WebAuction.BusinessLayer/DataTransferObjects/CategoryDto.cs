using System;
using WebAuction.BusinessLayer.DataTransferObjects.Common;

namespace WebAuction.BusinessLayer.DataTransferObjects
{
    public class CategoryDto : DtoBase
    {
        public string Name { get; set; }

        public bool HasParent => ParentId != null;

        /// <summary>
        /// Determines if products within this category 
        /// should be included in the search results
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Parent category Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Path reflecting category hierarchy, example: "Smartphones/Android"
        /// </summary>
        public string CategoryPath { get; set; }

        public CategoryDto Parent { get; set; }

        public override string ToString() => Name;
    }
}
