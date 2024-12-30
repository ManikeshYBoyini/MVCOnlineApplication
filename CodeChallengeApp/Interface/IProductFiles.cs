using CodeChallengeApp.Models;

namespace CodeChallengeApp.Interface
{
	public interface IProductFiles
	{
		Task<List<ProductDetail>> ReadProductDetailsFromFile();
		Task WriteProductDetailsToFile(IList<ProductDetail> productDetails);
		Task WriteImageToFolder(IFormFileCollection files, string fileName);
		Task DeleteWriteImageToFolder(IFormFileCollection files, string existingImageName, string newFileName);
	}
}
