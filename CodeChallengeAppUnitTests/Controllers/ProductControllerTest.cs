using CodeChallengeApp.Controllers;
using CodeChallengeApp.Interface;
using CodeChallengeApp.Models;
using CodeChallengeApp.ViewModel;
using CodeChallengeAppUnitTests.ControllersTestData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;

namespace CodeChallengeAppUnitTests.Controllers
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductResource> _productResourceMock;
        private readonly Mock<ICategoryResource> _categoryResourceMock;
        private readonly Mock<ISubCategoryResource> _subCategoryResourceMock;
        private readonly ProductController _productController;
        private readonly Mock<ILogger<ProductController>> _loggerMock;
        public ProductControllerTest()
        {
            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            
            this._productResourceMock = new Mock<IProductResource>();
            this._categoryResourceMock = new Mock<ICategoryResource>();            
            this._subCategoryResourceMock = new Mock<ISubCategoryResource>();
            this._loggerMock = new Mock<ILogger<ProductController>>();
            this._productController = new ProductController(_loggerMock.Object,
            _productResourceMock.Object,
           _categoryResourceMock.Object,
            _subCategoryResourceMock.Object)
            { TempData= tempData };
        }

        [Fact]
        public void Index_HappyPath()
        {
            //Arrange
            _productResourceMock.Setup(x => x.Products()).Returns(Task.FromResult<ProductCollection>(ProductsTestData.GetProductCollection().First()));

            //Act
            var productList = _productController.Index();
            var result= productList as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result?.Model);
        }

		[Fact]
		public void Index_ExceptionFlow()
		{
			//Arrange
			_productResourceMock.Setup(x => x.Products()).Throws<Exception>();

			//Act
			var productList = _productController.Index();
			var result = productList as ViewResult;

			//Assert
			Assert.NotNull(result);
			Assert.NotNull(result?.Model);
		}

		[Fact]
        public void Create_HappyPath()
        {
            //Arrange
            _subCategoryResourceMock.Setup(x => x.Subcategories()).Returns(SubCategoryTestData.GetSubCategories());
            _categoryResourceMock.Setup(x => x.Categories()).Returns(CategoryTestData.GetCategories());

            //Act
            var productList = _productController.Create();
            var result = productList as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result?.Model);
        }

		[Fact]
		public void Create_ExceptionFlow()
		{
			//Arrange
			_subCategoryResourceMock.Setup(x => x.Subcategories()).Throws<Exception>();
			_categoryResourceMock.Setup(x => x.Categories()).Returns(CategoryTestData.GetCategories());

			//Act
			var productList = _productController.Create();
			var result = productList as ViewResult;

			//Assert
			Assert.NotNull(result);
			Assert.NotNull(result?.Model);
		}

		[Fact]
        public void CreatePost_HappyPath()
        {
            //Arrange
            _subCategoryResourceMock.Setup(x => x.Subcategories()).Returns(SubCategoryTestData.GetSubCategories());
            _categoryResourceMock.Setup(x => x.Categories()).Returns(CategoryTestData.GetCategories());
            _productResourceMock.Setup(x => x.AddProduct(It.IsAny<ProductDetail>())).Returns(Task.FromResult<bool>(true));
            _productResourceMock.Setup(x => x.SaveFileToFolder(It.IsAny<IFormFileCollection>(),It.IsAny<string>()));
            ProductViewModel productCreateViewModel = ProductsTestData.GetProductViewModel();

            //Act
            var productList = _productController.Create(productCreateViewModel);

			//Assert
			Assert.NotNull(productList.Result);
        }

		[Fact]
		public void CreatePost_Exception()
		{
            //Arrange
            _subCategoryResourceMock.Setup(x => x.Subcategories()).Throws<Exception>();
			_categoryResourceMock.Setup(x => x.Categories()).Returns(CategoryTestData.GetCategories());
			_productResourceMock.Setup(x => x.AddProduct(It.IsAny<ProductDetail>())).Returns(Task.FromResult<bool>(true));
			_productResourceMock.Setup(x => x.SaveFileToFolder(It.IsAny<IFormFileCollection>(), It.IsAny<string>()));
			ProductViewModel productCreateViewModel = ProductsTestData.GetProductViewModel();

			//Act
			var productList = _productController.Create(productCreateViewModel);

			//Assert
			Assert.NotNull(productList.Result);
		}

		[Fact]
        public void Edit_HappyPath()
        {
            //Arrange
            _subCategoryResourceMock.Setup(x => x.Subcategories()).Returns(SubCategoryTestData.GetSubCategories());
            _categoryResourceMock.Setup(x => x.Categories()).Returns(CategoryTestData.GetCategories());

            //Act
            var productList = _productController.Edit(1);
            var result = productList as ViewResult;

			//Assert
			Assert.NotNull(result);
            Assert.NotNull(result?.Model);
        }

		[Fact]
		public void Edit_Exception()
		{
			//Arrange
			_subCategoryResourceMock.Setup(x => x.Subcategories()).Throws<Exception>();
			_categoryResourceMock.Setup(x => x.Categories()).Returns(CategoryTestData.GetCategories());

			//Act
			var productList = _productController.Edit(1);
			var result = productList as ViewResult;

			//Assert
			Assert.NotNull(result);
			Assert.NotNull(result?.Model);
		}

		[Fact]
        public void Edit_NotFound()
        {
            //Arrange
            _subCategoryResourceMock.Setup(x => x.Subcategories()).Returns(SubCategoryTestData.GetSubCategories());
            _categoryResourceMock.Setup(x => x.Categories()).Returns(CategoryTestData.GetCategories());

            //Act
            var productList = _productController.Edit(It.IsAny<int>());
            var result = productList as NotFoundResult;

			//Assert
			Assert.NotNull(result);
        }

        [Fact]
        public void EditPost_HappyPath()
        {
            //Arrange
            _subCategoryResourceMock.Setup(x => x.Subcategories()).Returns(SubCategoryTestData.GetSubCategories());
            _categoryResourceMock.Setup(x => x.Categories()).Returns(CategoryTestData.GetCategories());
            _productResourceMock.Setup(x => x.ProductById(1)).Returns(Task.FromResult<ProductCollection>(ProductsTestData.GetProductCollection().First()));
            _productResourceMock.Setup(x => x.UpdateProduct(It.IsAny<ProductDetail>())).Returns(Task.FromResult<bool>(true));
            _productResourceMock.Setup(x => x.UpdateFileToFolder(It.IsAny<IFormFileCollection>(), It.IsAny<string>(), It.IsAny<string>()));
            ProductViewModel productCreateViewModel = ProductsTestData.GetProductViewModel();

            //Act
            var productList = _productController.Edit(1,productCreateViewModel);

			//Assert
			Assert.NotNull(productList.Result);
        }

		[Fact]
		public void EditPost_Exception()
		{
			//Arrange
			_subCategoryResourceMock.Setup(x => x.Subcategories()).Throws<Exception>();
			_categoryResourceMock.Setup(x => x.Categories()).Returns(CategoryTestData.GetCategories());
			_productResourceMock.Setup(x => x.ProductById(1)).Returns(Task.FromResult<ProductCollection>(ProductsTestData.GetProductCollection().First()));
			_productResourceMock.Setup(x => x.UpdateProduct(It.IsAny<ProductDetail>())).Returns(Task.FromResult<bool>(true));
			_productResourceMock.Setup(x => x.UpdateFileToFolder(It.IsAny<IFormFileCollection>(), It.IsAny<string>(), It.IsAny<string>()));
			ProductViewModel productCreateViewModel = ProductsTestData.GetProductViewModel();

			//Act
			var productList = _productController.Edit(1, productCreateViewModel);

			//Assert
			Assert.NotNull(productList.Result);
		}
	}
}
