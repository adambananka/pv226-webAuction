﻿using System;
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

namespace WebAuction.BusinessLayer.Services.Categories
{
    public class CategoryService : CrudQueryServiceBase<Category, CategoryDto, CategoryFilterDto>, ICategoryService
    {
        public CategoryService(IMapper mapper, IRepository<Category> repository,
            QueryObjectBase<CategoryDto, Category, CategoryFilterDto, IQuery<Category>> query) : base(mapper,
            repository, query)
        {
        }

        protected override async Task<Category> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAccordingToNameAsync(string[] names)
        {
            var queryResult = await Query.ExecuteQuery(new CategoryFilterDto {Names = names});
            return queryResult.Items;
        }
    }
}