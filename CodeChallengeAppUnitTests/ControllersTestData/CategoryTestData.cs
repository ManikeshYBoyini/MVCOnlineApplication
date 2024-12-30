using CodeChallengeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallengeAppUnitTests.ControllersTestData
{
    public class CategoryTestData
    {
        public static IList<Category> GetCategories()
        {
            return new List<Category>()
                    { new Category() {Id=1, Name="Electronics" },
                      new Category() {Id=2, Name="Apparel" },
                      new Category() {Id=3, Name="Footwear" }
                    };
        }
    }
}
