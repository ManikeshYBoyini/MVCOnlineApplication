using CodeChallengeApp.Interface;
using CodeChallengeApp.Resources;
using CodeChallengeAppUnitTests.ControllersTestData;
using Moq;

namespace CodeChallengeAppUnitTests.Resources
{
	public class SubCategoryResourceTest
	{
		private readonly Mock<ISubCategoryCollection> _subCategoryCollection;
		private readonly ISubCategoryResource _subCategoryResource;

		public SubCategoryResourceTest()
		{
			this._subCategoryCollection = new Mock<ISubCategoryCollection>();
			this._subCategoryResource= new SubCategoryResource();
		}

		[Fact]
		public void Subcategories_HappyPath()
		{
			//arrange
			_subCategoryCollection.Setup(x => x.SubCategoryList()).Returns(SubCategoryTestData.GetSubCategories());

			//Act
			var subCategoryList = _subCategoryResource.Subcategories();

			//assert
			Assert.NotEmpty(subCategoryList);
			Assert.True(subCategoryList.Any());
		}

		[Fact]
		public void SubCatByCatId_HappyPath()
		{
			//arrange
			_subCategoryCollection.Setup(x => x.SubCategoryList()).Returns(SubCategoryTestData.GetSubCategories());

			//Act
			var subCategoryList = _subCategoryResource.SubCatByCatId(1);

			//assert
			Assert.NotEmpty(subCategoryList.Result);
			Assert.True(subCategoryList.Result.Any());
		}
	}
}
