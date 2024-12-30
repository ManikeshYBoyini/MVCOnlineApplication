using CodeChallengeApp.Interface;

namespace CodeChallengeApp.Models
{

    public sealed class SubcategoryCollection :ISubCategoryCollection
    {
        private SubcategoryCollection()
        {

        }
        private static ISubCategoryCollection instance = null;

        public static ISubCategoryCollection Instance()
        {
            if (instance == null)
            {
                instance = new SubcategoryCollection();
            }
            return instance;
        }
        public IList<Subcategory> SubCategoryList()
        {
            return new List<Subcategory>()
                    {
                        new Subcategory() { Id = 7, Name = "TV",CategoryId=1 },
                        new Subcategory() { Id = 2, Name = "Mobile",CategoryId=1 },
                        new Subcategory() { Id = 3, Name = "Refrigerator",CategoryId=1 },
                        new Subcategory() { Id = 1, Name = "Men’s Cloth",CategoryId=2 },
                        new Subcategory() { Id = 4, Name = "Women’s cloth" ,CategoryId=2},
                        new Subcategory() { Id = 5, Name = "Men’s Footwear",CategoryId=3 },
                        new Subcategory() { Id = 6, Name = "kid’s footwears",CategoryId=3 }
                    };
        }
    }
    public class Subcategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get;set; }
    }
}
