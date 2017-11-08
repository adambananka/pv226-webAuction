using AutoMapper;
using Moq;
using WebAuction.BusinessLayer.Config;
using WebAuction.BusinessLayer.DataTransferObjects.Common;
using WebAuction.BusinessLayer.QueryObjects.Common;
using WebAuction.Infrastructure;
using WebAuction.Infrastructure.Query;
using WebAuction.Infrastructure.UnitOfWork;

namespace WebAuction.BusinessLayer.Tests.FacadeTests.Common
{
    public class FacadeMockManager
    {
        internal static IMapper GetMapper()
        {
            return new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
        }

        internal static Mock<IUnitOfWorkProvider> GetUowMock()
        {
            var uowMock = new Mock<IUnitOfWorkProvider>();
            uowMock.Setup(provider => provider.Create()).Returns(new StubUow());

            return uowMock;
        }

        internal Mock<IRepository<TEntity>> GetRepositoryMock<TEntity>() where TEntity : class, IEntity, new()
        {
            return new Mock<IRepository<TEntity>>();
        }

        internal Mock<QueryObjectBase<TDto, TEntity, TFilterDto, IQuery<TEntity>>> GetQueryObjectMock
            <TDto, TEntity, TFilterDto>(QueryResultDto<TDto, TFilterDto> toReturn)
            where TEntity : class, IEntity, new()
            where TFilterDto : FilterDtoBase
        {
            var queryObjectMock = new Mock<QueryObjectBase<TDto, TEntity, TFilterDto, IQuery<TEntity>>>(null, null);
            queryObjectMock.Setup(queryObject => queryObject.ExecuteQuery(It.IsAny<TFilterDto>()))
                .ReturnsAsync(toReturn);

            return queryObjectMock;
        }
    }
}
