using CodeChallengeApp.Controllers;
using CodeChallengeApp.Interface;
using CodeChallengeAppUnitTests.ControllersTestData;
using Moq;

namespace CodeChallengeAppUnitTests.Controllers
{
    public class CategoryControllerTest
    {
        private readonly Mock<ICategoryResource> _categoryResourceMock;
        private readonly CategoryController _categoryController;
        public CategoryControllerTest()
        {
            this._categoryResourceMock = new Mock<ICategoryResource>();
            this._categoryController = new CategoryController( _categoryResourceMock.Object);
        }

        [Fact]
        public void GetCategories_HappyPath()
        {
            //arrange
            _categoryResourceMock.Setup(x => x.Categories()).Returns(CategoryTestData.GetCategories());

            //Act
            var categoryList = _categoryController.Get();

            //assert
            Assert.NotEmpty(categoryList);
            Assert.True(categoryList.Any());
        }
    }
}
