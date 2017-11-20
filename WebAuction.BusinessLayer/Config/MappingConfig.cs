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
            config.CreateMap<Bid, BidDto>().ReverseMap();
            config.CreateMap<User, UserDto>().ReverseMap();
            config.CreateMap<Rating, RatingDto>().ReverseMap();
            config.CreateMap<Comment, CommentDto>().ReverseMap();
            config.CreateMap<Payment, PaymentDto>().ReverseMap();
            config.CreateMap<Auction, AuctionDto>().ReverseMap();
            config.CreateMap<QueryResult<User>, QueryResultDto<UserDto, UserFilterDto>>();
            config.CreateMap<QueryResult<Category>, QueryResultDto<CategoryDto, CategoryFilterDto>>();
            config.CreateMap<QueryResult<Bid>, QueryResultDto<BidDto, BidFilterDto>>();
            config.CreateMap<QueryResult<Rating>, QueryResultDto<RatingDto, RatingFilterDto>>();
            config.CreateMap<QueryResult<Comment>, QueryResultDto<CommentDto, CommentFilterDto>>();
            config.CreateMap<QueryResult<Payment>, QueryResultDto<PaymentDto, PaymentFilterDto>>();
            config.CreateMap<QueryResult<Auction>, QueryResultDto<AuctionDto, AuctionFilterDto>>();
        }
    }
}