using System;
using System.Linq;
using System.Threading.Tasks;
using WebAuction.DataAccessLayer.EntityFramework.Entities;
using WebAuction.Infrastructure;
using WebAuction.Infrastructure.UnitOfWork;
using Xunit;
using Xunit.Abstractions;

namespace WebAuction.DataAccessLayer.EntityFramework.Tests.RepositoryTests
{
    public class CategoryRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IUnitOfWorkProvider _unitOfWorkProvider;
        private readonly IRepository<Category> _categoryRepository;

        private readonly Guid _electronicsCategoryId = Guid.Parse("00000001-a1de-42ca-ae58-0083fa9f0d7f");
        private readonly Guid _smartphonesCategoryId = Guid.Parse("00000011-a1de-42ca-ae58-0083fa9f0d7f");

        public CategoryRepositoryTests(DatabaseFixture databaseFixture, ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _unitOfWorkProvider = databaseFixture.WindsorContainer.Resolve<IUnitOfWorkProvider>();
            _categoryRepository = databaseFixture.WindsorContainer.Resolve<IRepository<Category>>();
        }

        [Fact]
        public async Task GetCategoryAsync_StoredInDbNoIncludes_ReturnsCorrectCategory()
        {
            Category smartphoneCategory;

            using (_unitOfWorkProvider.Create())
            {
                smartphoneCategory = await _categoryRepository.GetAsync(_smartphonesCategoryId);
            }

            Assert.Equal(_smartphonesCategoryId, smartphoneCategory.Id);

            // Just a tidbit
            var notIncludedProperties = typeof(Category).GetProperties()
                .Where(info => info.PropertyType.Assembly.FullName.Contains("WebAuction"));
            foreach (var property in notIncludedProperties)
            {
                _testOutputHelper.WriteLine(smartphoneCategory.Parent != null ? "1" : "2");
                Assert.Null(property.GetValue(smartphoneCategory, null));
            }
        }

        [Fact]
        public async Task GetCategoryAsync_StoredInDbWithIncludes_ReturnsCorrectCategory()
        {
            Category smartphoneCategory;

            using (_unitOfWorkProvider.Create())
            {
                smartphoneCategory = await _categoryRepository.GetAsync(_smartphonesCategoryId);
            }

            Assert.Equal(_smartphonesCategoryId, smartphoneCategory.Id);
            Assert.NotNull(smartphoneCategory.Parent);
            Assert.Equal(_electronicsCategoryId, smartphoneCategory.ParentId);
        }
    }
}
