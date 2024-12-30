using CodeChallengeApp.Interface;
using CodeChallengeApp.Resources;
using CodeChallengeAppUnitTests.ControllersTestData;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallengeAppUnitTests.Resources
{
	public class CategoryResourceTest
	{
		private readonly Mock<ICategoryCollection> _categoryCollection;
		private readonly ICategoryResource _categoryResource;

		public CategoryResourceTest()
		{
			this._categoryCollection = new Mock<ICategoryCollection>();
			this._categoryResource = new CategoryResource();
		}

		[Fact]
		public void categories_HappyPath()
		{
			//arrange
			_categoryCollection.Setup(x => x.CategoryList()).Returns(CategoryTestData.GetCategories());

			//Act
			var categoryList = _categoryResource.Categories();

			//assert
			Assert.NotEmpty(categoryList);
			Assert.True(categoryList.Any());
		}
	}
}
