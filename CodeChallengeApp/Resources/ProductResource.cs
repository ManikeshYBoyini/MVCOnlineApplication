using CodeChallengeApp.Interface;
using CodeChallengeApp.Models;
using CodeChallengeApp.ViewModel;
using Newtonsoft.Json;

namespace CodeChallengeApp.Resources
{
    public class ProductResource : IProductResource
    {
        #region Private variable
        private readonly IProductFiles _productFiles;

        #endregion

        #region Constructor(s)
        public ProductResource(IProductFiles productFiles)
        {
            this._productFiles = productFiles;
		}

		#endregion

		#region Public members

        /// <summary>
        /// Add product to the file
        /// </summary>
        /// <param name="product"></param>
        /// <returns>true of false</returns>
		public async Task<bool> AddProduct(ProductDetail product)
        {
            var products = await _productFiles.ReadProductDetailsFromFile();
            if (products != null)
            {
                int currentId = products.Max(x => x.Id) + 1;
                product.Id = currentId;
                products.Add(product);
                await _productFiles.WriteProductDetailsToFile(products);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Fetch all the products 
        /// </summary>
        /// <returns>List of ProductCollection</returns>
        public async Task<ProductCollection> Products()
        {
            var productsList = await _productFiles.ReadProductDetailsFromFile();
			return new ProductCollection() { List = productsList };            
        }

        /// <summary>
        /// Fetch products by category and sub category
        /// </summary>
        /// <param name="productFilterModel"></param>
        /// <returns>list of ProductCollection</returns>
		public async Task<ProductCollection> ProductsByFilter(ProductsFilterModel productFilterModel)
		{
            var products = await Products();
            var result= products.List
                    .Where(x=>x.CategoryId== productFilterModel.CategoryId && 
                    x.SubCategoryId==productFilterModel.SubCategoryId).ToList();
            return new ProductCollection() { List = result };
		}

        /// <summary>
        /// Fetch product by product id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>list of ProductCollection</returns>
        public async Task<ProductCollection> ProductById(int productId)
        {
            var products = await Products();
            var result= products.List.Where(x=>x.Id==productId).ToList();
			return new ProductCollection() { List = result };

		}

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>true or false</returns>
        public async Task<bool> UpdateProduct(ProductDetail product)
        {
            if (product?.Id>0)
            {
                var listOfProducts = await Products();
					listOfProducts.List.Remove(listOfProducts.List.First(x=>x.Id==product.Id));
                    listOfProducts.List.Add(product);
                await _productFiles.WriteProductDetailsToFile(listOfProducts.List);
                return true;
			}
            return false;
        }

        /// <summary>
        /// Saves image from ui to wwwroot/images/ folder
        /// </summary>
        /// <param name="files"></param>
        /// <param name="fileName"></param>
        public async Task SaveFileToFolder(IFormFileCollection files,string fileName)
        {
            await _productFiles.WriteImageToFolder(files, fileName);
		}

        /// <summary>
        /// Updates image in wwwroot/images folder
        /// </summary>
        /// <param name="files"></param>
        /// <param name="existingImageName"></param>
        /// <param name="newFileName"></param>
		public async Task UpdateFileToFolder(IFormFileCollection files,string existingImageName,string newFileName)
		{
			await _productFiles.DeleteWriteImageToFolder(files,existingImageName, newFileName);
		}

		#endregion

		#region Private members
		
		#endregion
	}
}
