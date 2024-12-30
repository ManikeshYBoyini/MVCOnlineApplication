using CodeChallengeApp.Models;
using CodeChallengeApp.Interface;

namespace CodeChallengeApp.Resources
{
	public class CategoryResource :ICategoryResource
	{
		/// <summary>
		/// Fetch all available categories 
		/// </summary>
		/// <returns>List of category</returns>
		public IList<Category> Categories()
		{
			return CategoryCollection.Instance().CategoryList().OrderBy(x=>x.Id).ToList();
		}

	}
}
