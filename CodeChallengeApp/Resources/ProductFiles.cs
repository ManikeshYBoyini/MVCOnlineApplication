using CodeChallengeApp.Interface;
using CodeChallengeApp.Models;
using Microsoft.Build.Framework;
using Newtonsoft.Json;

namespace CodeChallengeApp.Resources
{
	public class ProductFiles :IProductFiles
	{
		#region Private members
		private readonly string filePath;
		private readonly string imagesPath;
		private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

		#endregion

		#region Constructor(s)

		public ProductFiles(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
		{
			_hostingEnvironment = hostingEnvironment;
			filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Resources\\productTable.json");
			imagesPath = Path.Combine(_hostingEnvironment.WebRootPath, "images\\");
		}

		#endregion

		#region Public members

		/// <summary>
		/// Reads productdetails from json file
		/// </summary>
		/// <returns>List of ProductDetail object</returns>
		public async Task<List<ProductDetail>> ReadProductDetailsFromFile()
		{
			var json = File.ReadAllText(filePath);
			var products = JsonConvert.DeserializeObject<List<ProductDetail>>(json);
			return products;
		}

		/// <summary>
		/// Write product details to json file
		/// </summary>
		/// <param name="productDetails"></param>
		public async Task WriteProductDetailsToFile(IList<ProductDetail> productDetails)
		{
			var convertedJson = JsonConvert.SerializeObject(productDetails, Formatting.Indented);
			File.WriteAllText(filePath, convertedJson);
		}

		/// <summary>
		/// Write product image to wwwroot/images folder
		/// </summary>
		/// <param name="files"></param>
		/// <param name="fileName"></param>
		public async Task WriteImageToFolder(IFormFileCollection files,string fileName)
		{
			var file = files.FirstOrDefault();
			if (file != null)
			{
				var fileNamePath = Path.Combine(imagesPath, fileName);
				using (var fileStream = new FileStream(fileNamePath, FileMode.Create))
				{
					try
					{
						await file.CopyToAsync(fileStream);
					}
					catch { }
				}
			}
		}

		/// <summary>
		/// Update product image in wwwroot/images folder
		/// </summary>
		/// <param name="files"></param>
		/// <param name="existingImageName"></param>
		/// <param name="newFileName"></param>
		public async Task DeleteWriteImageToFolder(IFormFileCollection files, string existingImageName, string newFileName)
		{
			var file = files.FirstOrDefault();
			if (file != null)
			{
				await WriteImageToFolder(files, newFileName);
				if (!string.IsNullOrEmpty(existingImageName) && File.Exists(Path.Combine(imagesPath, existingImageName)))
				{
					File.Delete(Path.Combine(imagesPath, existingImageName));
				}
			}
		}

		#endregion
	}
}
