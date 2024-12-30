using CodeChallengeApp.Interface;

namespace CodeChallengeApp.Models
{
    public sealed class CategoryCollection : ICategoryCollection
	{
        private CategoryCollection()
        {
        }

        private static ICategoryCollection instance = null;

		public static ICategoryCollection Instance()
		{
			if (instance == null)
			{
				instance = new CategoryCollection();
			}
			return instance;
		}

        public IList<Category> CategoryList()
        {
            return new List<Category>()
            { new Category() {Id=1, Name="Electronics" },
                      new Category() {Id=2, Name="Apparel" },
                      new Category() {Id=3, Name="Footwear" }
            };
        }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
