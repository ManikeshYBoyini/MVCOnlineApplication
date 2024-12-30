using CodeChallengeApp.Models;
using CodeChallengeApp.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallengeAppUnitTests.ControllersTestData
{
    public class ProductsTestData
    {
        public static IList<ProductCollection> GetProductCollection()
        {
            return new List<ProductCollection>()
                    { new ProductCollection() { List=
                                        new List<ProductDetail>(){
                                            new ProductDetail{ Id=1,Name="produc1",Description="desc",Price=12,Quantity=12} },
                      }};
        }

        public static ProductViewModel GetProductViewModel()
        {
           return  new ProductViewModel()
            {
                Id = 1,
                CategoryId = 1,
                SubCategoryId = 1,
                Description = "test",
                Price = 1,
                Quantity = 1,
                Name = "test",
                Category = new List<SelectListItem>() { new SelectListItem() { Text = "test", Value = "1" } },
                SubCategory = new List<SelectListItem>() { new SelectListItem() { Text = "test", Value = "1" } },
            };
        }
    }
}
