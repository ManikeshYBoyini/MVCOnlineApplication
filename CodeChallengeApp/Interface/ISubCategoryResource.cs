using CodeChallengeApp.Models;

namespace CodeChallengeApp.Interface
{
	public interface ISubCategoryResource
	{
		IList<Subcategory> Subcategories();
		Task<IList<Subcategory>> SubCatByCatId(int categoryId);
	}
}
