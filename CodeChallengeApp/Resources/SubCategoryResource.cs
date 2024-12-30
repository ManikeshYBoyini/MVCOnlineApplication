using CodeChallengeApp.Interface;
using CodeChallengeApp.Models;

namespace CodeChallengeApp.Resources
{
	public class SubCategoryResource :ISubCategoryResource
	{
		#region Public members
		/// <summary>
		/// Fetch list of sub categories
		/// </summary>
		/// <returns>List of SubCategory</returns>
		public IList<Subcategory> Subcategories()
		{
			return SubcategoryCollection.Instance().SubCategoryList().OrderBy(x=>x.Id).ToList();
		}

		/// <summary>
		/// Fetch sub categories by category selected
		/// </summary>
		/// <param name="categoryId"></param>
		/// <returns>List of SubCategory</returns>
		public async Task<IList<Subcategory>> SubCatByCatId(int categoryId)
		{
			return SubcategoryCollection.Instance().SubCategoryList().Where(x => x.CategoryId == categoryId).ToList();
		}
		#endregion
	}
}
