using CodeChallengeApp.Controllers;
using CodeChallengeApp.Interface;
using CodeChallengeApp.Models;
using CodeChallengeAppUnitTests.ControllersTestData;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;

namespace CodeChallengeAppUnitTests.Controllers
{
    public class SubCategoryControllerTest
    {
        private readonly Mock<ISubCategoryResource> _subCategoryResourceMock;
        private readonly SubCategoryController _subCategoryController;
		private readonly Mock<ILogger<SubCategoryController>> _loggerMock;
		public SubCategoryControllerTest()
        {
            this._subCategoryResourceMock= new Mock<ISubCategoryResource>();
			this._loggerMock = new Mock<ILogger<SubCategoryController>>();
			this._subCategoryController = new SubCategoryController( _subCategoryResourceMock.Object,_loggerMock.Object);
        }

        [Fact]
        public void GetSubCategories_HappyPath()
        {
            //arrange
            _subCategoryResourceMock.Setup(x => x.Subcategories()).Returns(SubCategoryTestData.GetSubCategories());
            
            //Act
            var categoryList= _subCategoryController.Get();

            //assert
            Assert.NotEmpty(categoryList);
            Assert.True(categoryList.Any());
        }

        [Fact]
        public void GetSubCatByCatId_HappyPath()
        {
            //arrange
            _subCategoryResourceMock.Setup(x => x.SubCatByCatId(It.IsAny<int>())).Returns(Task.FromResult<IList<Subcategory>>(SubCategoryTestData.GetSubCategories()));

            //Act
            var categoryList = _subCategoryController.GetSubCatByCatId(It.IsAny<int>());

            //assert
            Assert.NotEmpty(categoryList.Result);
            Assert.True(categoryList.Result.Any());
        }
    }
}
