using CodeChallengeApp.Models;
using CodeChallengeApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallengeAppUnitTests.ResourcesTestData
{
	internal class ProductResourceTestData
	{
		public static IList<ProductDetail> GetProductDetails()
		{
			return new List<ProductDetail>() {										
											new ProductDetail{ Id=1,Name="produc1",Description="desc",Price=12,Quantity=12,Code="23",SubCategoryId=1,CategoryId=1},
											new ProductDetail{ Id=2,Name="product2",Description="desc",Price=12,Quantity=12,Code="12",SubCategoryId=1,CategoryId=1}
					  };
		}

		public static ProductsFilterModel GetProductsFilterModel()
		{
			return new ProductsFilterModel()
			{
				CategoryId = 1,
				SubCategoryId = 1
			};
		}

		public static ProductDetail GetProductDetail()
		{
			return new ProductDetail { Id = 1, Name = "produc1", Description = "desc", Price = 12, Quantity = 12, Code = "23", SubCategoryId = 1, CategoryId = 1 };
		}
	}
}
