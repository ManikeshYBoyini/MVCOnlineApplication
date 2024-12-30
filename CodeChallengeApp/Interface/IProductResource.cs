using CodeChallengeApp.Models;
using CodeChallengeApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallengeApp.Interface
{
    public interface IProductResource
    {
		Task<bool> AddProduct(ProductDetail product);
		Task<ProductCollection> Products();
		Task<ProductCollection> ProductsByFilter(ProductsFilterModel productFilterModel);
		Task<bool> UpdateProduct(ProductDetail product);
		Task<ProductCollection> ProductById(int productId);
		Task SaveFileToFolder(IFormFileCollection files, string fileName);
		Task UpdateFileToFolder(IFormFileCollection files, string existingImageName, string newFileName);


	}
}
