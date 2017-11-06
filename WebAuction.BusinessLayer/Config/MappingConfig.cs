using AutoMapper;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Common;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using WebAuction.DataAccessLayer.EntityFramework.Entities;
using WebAuction.Infrastructure.Query;

namespace WebAuction.BusinessLayer.Config
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Category, CategoryDto>()
                .ForMember(categoryDto => categoryDto.CategoryPath, opts => opts.ResolveUsing(category =>
                {
                    var categoryPath = category.Name;
                    while (category.Parent != null)
                    {
                        categoryPath = category.Parent.Name + "/" + categoryPath;
                        category = category.Parent;
                    }
                    return categoryPath;
                })).ReverseMap();
            config.CreateMap<QueryResult<Category>, QueryResultDto<CategoryDto, CategoryFilterDto>>();
        }
    }
}