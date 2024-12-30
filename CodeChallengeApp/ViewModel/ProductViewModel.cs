using CodeChallengeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodeChallengeApp.ViewModel
{
	public class ProductViewModel
    {
        public int Id { get; set; }

		public string Name { get; set; }

		[Required(ErrorMessage = "Quantity is required")]
		[Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000")]
        public int Quantity { get; set; }

        public string Code { get; set; }


		[Required(ErrorMessage = "Price is required")]
		[RegularExpression(@"^\d+(\.\d{1,2})?$")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageFileName { get; set; } = string.Empty;

		public int CategoryId { get; set; }
		public int SubCategoryId { get; set; }
		public IEnumerable<SelectListItem> Category { get; set; } = default!;
		public IEnumerable<SelectListItem> SubCategory { get; set; } = default!;

	}
}
