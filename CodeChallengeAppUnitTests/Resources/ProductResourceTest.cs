using CodeChallengeApp.Interface;
using CodeChallengeApp.Models;
using CodeChallengeApp.Resources;
using CodeChallengeAppUnitTests.ResourcesTestData;
using Microsoft.AspNetCore.Http;
using Moq;

namespace CodeChallengeAppUnitTests.Resources
{
	public class ProductResourceTest
	{
		private readonly Mock<IProductFiles> _productFilesMock;
		private readonly IProductResource _productResource;
		public ProductResourceTest()
		{
			this._productFilesMock = new Mock<IProductFiles>();
			this._productResource = new ProductResource(_productFilesMock.Object);
		}

		[Fact]
		public void AddProduct_HappyPath()
		{
			//Arrange
			_productFilesMock.Setup(x => x.ReadProductDetailsFromFile())
				.Returns(Task.FromResult<List<ProductDetail>>(ProductResourceTestData.GetProductDetails().ToList()));

			//Act
			var productList = _productResource.AddProduct(new ProductDetail());
			var result = productList.Result;

			//Assert
			Assert.True(result);
		}

		[Fact]
		public void Products_HappyPath()
		{
			//Arrange
			_productFilesMock.Setup(x => x.ReadProductDetailsFromFile())
				.Returns(Task.FromResult<List<ProductDetail>>(ProductResourceTestData.GetProductDetails().ToList()));

			//Act
			var productList = _productResource.Products();
			var result = productList.Result;

			//Assert
			Assert.True(result.List.Any());
		}

		[Fact]
		public void ProductsByFilter_HappyPath()
		{
			//Arrange
			_productFilesMock.Setup(x => x.ReadProductDetailsFromFile())
				.Returns(Task.FromResult<List<ProductDetail>>(ProductResourceTestData.GetProductDetails().ToList()));

			//Act
			var productList = _productResource.ProductsByFilter(ProductResourceTestData.GetProductsFilterModel());
			var result = productList.Result;

			//Assert
			Assert.True(result.List.Any());
		}

		[Fact]
		public void ProductById_HappyPath()
		{
			//Arrange
			_productFilesMock.Setup(x => x.ReadProductDetailsFromFile())
				.Returns(Task.FromResult<List<ProductDetail>>(ProductResourceTestData.GetProductDetails().ToList()));

			//Act
			var productList = _productResource.ProductById(1);
			var result = productList.Result;

			//Assert
			Assert.True(result.List.Any());
		}

		[Fact]
		public void UpdateProduct_HappyPath()
		{
			//Arrange
			_productFilesMock.Setup(x => x.WriteProductDetailsToFile(It.IsAny<List<ProductDetail>>()));
			_productFilesMock.Setup(x => x.ReadProductDetailsFromFile())
				.Returns(Task.FromResult<List<ProductDetail>>(ProductResourceTestData.GetProductDetails().ToList()));

			//Act
			var productList = _productResource.UpdateProduct(ProductResourceTestData.GetProductDetail());
			var result = productList.Result;

			//Assert
			Assert.True(result);
		}

		[Fact]
		public void SaveFileToFolder_HappyPath()
		{
			//Arrange
			_productFilesMock.Setup(x => x.WriteImageToFolder(It.IsAny<IFormFileCollection>(),It.IsAny<string>()));

			//Act
			_productResource.SaveFileToFolder(It.IsAny<IFormFileCollection>(), It.IsAny<string>());

			//Assert
			Assert.True(true);
		}

		[Fact]
		public void UpdateFileToFolder_HappyPath()
		{
			//Arrange
			_productFilesMock.Setup(x => x.DeleteWriteImageToFolder(It.IsAny<IFormFileCollection>(), It.IsAny<string>(), It.IsAny<string>()));

			//Act
			_productResource.UpdateFileToFolder(It.IsAny<IFormFileCollection>(), It.IsAny<string>(), It.IsAny<string>());

			//Assert
			Assert.True(true);
		}
	}

}
