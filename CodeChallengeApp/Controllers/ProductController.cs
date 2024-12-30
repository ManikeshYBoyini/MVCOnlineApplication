using CodeChallengeApp.Interface;
using CodeChallengeApp.Models;
using CodeChallengeApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace CodeChallengeApp.Controllers
{
    public class ProductController : Controller
    {
		#region Private variables
		private readonly IProductResource _productResource;
        private readonly ILogger<ProductController> _logger;
        private readonly ICategoryResource _categoryResource;
        private readonly ISubCategoryResource _subCategoryResource;

		#endregion

		#region Constructors
		public ProductController(ILogger<ProductController> logger,
            IProductResource productResource,
            ICategoryResource category,
            ISubCategoryResource subCategoryResource
			)
        {
            this._productResource = productResource;
            this._logger= logger;
            this._categoryResource = category;
            this._subCategoryResource = subCategoryResource; 
        }

		#endregion

		#region public members

		/// <summary>
		/// Initial page load: loads all the products data to the products home page
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
            try
            {
                List<ProductViewModel> productListViewModelList = new List<ProductViewModel>();
                var productList = this._productResource.Products().Result.List;

                //Check if there are any products available in system
                if (productList != null)
                {
                    foreach (var item in productList)
                    {
                        ProductViewModel productListViewModel = new ProductViewModel();
                        productListViewModel.Id = item.Id;
                        productListViewModel.Name = item.Name;
                        productListViewModel.Description = item.Description;
                        productListViewModel.Code = item.Code;
                        productListViewModel.Price = item.Price;
                        productListViewModel.CategoryId = item.CategoryId;
                        productListViewModel.ImageFileName = item.ImageFileName;
                        productListViewModel.Quantity = item.Quantity;
                        productListViewModelList.Add(productListViewModel);

                    }
                }
                _logger.LogInformation("Product/Index: Response data: " + JsonConvert.SerializeObject(productListViewModelList, Formatting.Indented));
                return View(productListViewModelList.OrderBy(x=>x.Name).ToList());
            }
            catch(Exception ex)
            {
                _logger.LogError($"Index load method, Error message {ex.Message}"); ErrorViewModel err = new ErrorViewModel()
				{
					RequestId = "Unable to load home page"
				};
				return View("Error", err);
			}
		}

        /// <summary>
        /// Initial load of product create page
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            try
            {
                ProductViewModel productCreateViewModel = new ProductViewModel();

                //Add category dropdown values
                productCreateViewModel.Category = (IEnumerable<SelectListItem>)this._categoryResource.Categories().Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });

                //Add sub category dropdown values
                productCreateViewModel.SubCategory = (IEnumerable<SelectListItem>)this._subCategoryResource.Subcategories().Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });

                return View(productCreateViewModel);
            }
            catch(Exception ex)
            {
				_logger.LogError($"Product create method, Error message {ex.Message}"); ErrorViewModel err = new ErrorViewModel()
				{
					RequestId = "Unable to load create product page"
				};
				return View("Error", err);
			}
        }

        /// <summary>
        /// Product resource creation post method
        /// </summary>
        /// <param name="productCreateViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [RequestSizeLimit(1000000)]
        public async Task<IActionResult> Create(ProductViewModel productCreateViewModel)
        {
            try
            {
                _logger.LogInformation("Product/Create(post): Response data: " + JsonConvert.SerializeObject(productCreateViewModel, Formatting.Indented));
                ModelState.Remove("Category");
                ModelState.Remove("SubCategory");
                ModelState.Remove("Code");
                ModelState.Remove("Description");
                ModelState.Remove("ImageFileName");

                //Check is the model is valid or not
                if (ModelState.IsValid)
                {
                    var files = this.Request?.Form.Files;

                    //Add category dropdown values
                    productCreateViewModel.Category = (IEnumerable<SelectListItem>)this._categoryResource.Categories()
                                                                                        .Select(c => new SelectListItem()
                                                                                        {
                                                                                            Text = c.Name,
                                                                                            Value = c.Id.ToString()
                                                                                        });

                    //Add subcategory dropdown values
                    productCreateViewModel.SubCategory = (IEnumerable<SelectListItem>)this._subCategoryResource
                                                                            .Subcategories().Select(c => new SelectListItem()
                                                                            {
                                                                                Text = c.Name,
                                                                                Value = c.Id.ToString()
                                                                            });

                    //create a productdetail object
                    var product = new ProductDetail();
                    product.Name = productCreateViewModel.Name;
                    product.Description = productCreateViewModel.Description;
                    product.Price = productCreateViewModel.Price;
                    product.Code = productCreateViewModel.Code;
                    product.Quantity = productCreateViewModel.Quantity;
                    product.CategoryId = productCreateViewModel.CategoryId;
                    product.SubCategoryId = productCreateViewModel.SubCategoryId;
                    product.ImageFileName = ((files != null && files.Count > 0) ? files[0].FileName : "");

                    await this._productResource.AddProduct(product);
                    this._productResource.SaveFileToFolder(files, product.ImageFileName);

                    ViewData["ProductSuccessMsg"] = "Product (" + product.Name + ") added successfully.";
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    productCreateViewModel = GetProductViewModel();
                }
                _logger.LogInformation("Product/Create(post): Response data: " + JsonConvert.SerializeObject(productCreateViewModel, Formatting.Indented));
                return View(productCreateViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Product create method, Error message {ex.Message}"); ErrorViewModel err = new ErrorViewModel()
                {
                    RequestId = "Unable to add the product"
                };
                return View("Error", err);
            }
        }

        /// <summary>
        /// Initial load of product with existing values
        /// </summary>
        /// <param name="id"></param>
        /// <returns>edit view</returns>
		public IActionResult Edit(int id)
		{
            try
            {
				if (id > 0)
                {
                    var productToEdit = this._productResource.ProductById(id).Result.List.FirstOrDefault();
                    if (productToEdit != null)
                    {
                        var productViewModel = new ProductViewModel()
                        {
                            Id = productToEdit.Id,
                            Name = productToEdit.Name,
                            Description = productToEdit.Description,
                            Price = productToEdit.Price,
                            CategoryId = productToEdit.CategoryId,
                            Code = productToEdit.Code,
                            ImageFileName = productToEdit.ImageFileName,
                            SubCategoryId = productToEdit.SubCategoryId,
                            Quantity = productToEdit.Quantity,
							Category = (IEnumerable<SelectListItem>)this._categoryResource.Categories()
                                                                        .Select(c => new SelectListItem()
							{
								Text = c.Name,
								Value = c.Id.ToString()
							}),
							SubCategory = (IEnumerable<SelectListItem>)this._subCategoryResource.Subcategories()
                                                                        .Select(c => new SelectListItem()
							{
								Text = c.Name,
								Value = c.Id.ToString()
							})
						};
                        return View(productViewModel);
                    }
                    return NotFound();
                }
                else
                {
                    return NotFound();
                }
            }
			catch (Exception ex)
			{
				_logger.LogError($"Product edit method, Error message {ex.Message}"); ErrorViewModel err = new ErrorViewModel()
				{
					RequestId = "Unable to load edit view"
				};
				return View("Error", err);
			}
		}

        /// <summary>
        /// Updates the productViewModel 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productViewModel"></param>
        /// <returns>Returns edit view</returns>
		[HttpPost]
		[RequestSizeLimit(1000000)]
		public async Task<IActionResult> Edit(int id, ProductViewModel productViewModel)
		{
            try
            {
				ModelState.Remove("Category");
				ModelState.Remove("SubCategory");
				ModelState.Remove("Code");
				ModelState.Remove("Description");
				ModelState.Remove("ImageFileName");

                //Check if model is valid or not
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Product/Create(post): Response data: " + JsonConvert.SerializeObject(productViewModel, Formatting.Indented));
                    var files = this.Request?.Form?.Files;
                    var existingProduct = this._productResource.ProductById(productViewModel.Id).Result.List;

                    //Add category dropdown values
                    productViewModel.Category = this._categoryResource.Categories().Select(c => new SelectListItem()
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    });

                    //Add sub category drop down values
                    productViewModel.SubCategory = this._subCategoryResource.Subcategories().Select(c => new SelectListItem()
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    });
                    var product = new ProductDetail()
                    {
                        Id = productViewModel.Id,
                        Name = productViewModel.Name,
                        Description = productViewModel.Description,
                        Price = productViewModel.Price,
                        Code = productViewModel.Code,
                        Quantity = productViewModel.Quantity,
                        CategoryId = productViewModel.CategoryId,
                        ImageFileName = ((files != null && files.Count > 0) ? files[0].FileName : existingProduct.First().ImageFileName),
                        SubCategoryId = productViewModel.SubCategoryId
                    };
                    await this._productResource.UpdateProduct(product);

                    if ((string.IsNullOrEmpty(existingProduct.First().ImageFileName) && !(string.IsNullOrEmpty(product.ImageFileName)))
                        || (!string.Equals(existingProduct.First().ImageFileName, product.ImageFileName)))
                    {
                        this._productResource.UpdateFileToFolder(files, existingProduct.First().ImageFileName, product.ImageFileName);
                    }

                    ViewData["ProductSuccessMsg"] = "Product (" + product.Name + ") updated successfully !";
                }
                else
                {
                    productViewModel = GetProductViewModel();
                }
				_logger.LogInformation("Product/Edit: Response data: " + JsonConvert.SerializeObject(productViewModel, Formatting.Indented));
				return View(productViewModel);
            }

            catch (Exception ex)
            {
                _logger.LogError($"Product edit method, Error message {ex.Message}");
                ErrorViewModel err = new ErrorViewModel()
                {
                    RequestId = "Unable to save the product"
                };
                return View("Error", err);
            }
		}

        /// <summary>
        /// Get product list(s) by category and subcategory filters
        /// </summary>
        /// <param name="productFilter"></param>
        /// <returns></returns>
		[HttpPost]
        public async Task<IActionResult> ProductByFilter([FromBody] ProductsFilterModel productFilter)
        {
            try
            {
                var productCollection = await this._productResource.ProductsByFilter(productFilter);
                var productList = productCollection.List;

                List<ProductViewModel> products = new List<ProductViewModel>();
                foreach (var product in productList)
                {
                    products.Add(new ProductViewModel()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Code = product.Code,
                        CategoryId = product.CategoryId,
                        ImageFileName = product.ImageFileName,
                        SubCategoryId = product.SubCategoryId
                    });
                }
                ModelState.Clear();

                if (products.Count < 1)
                {
                    ViewData["SearchProductSuccessMsg"] = "No Product found!";
                }

				_logger.LogInformation("Product/ProductByFilter: Response data: " + JsonConvert.SerializeObject(products, Formatting.Indented));
				return PartialView("~/Views/Shared/_ProductsList.cshtml", products.OrderBy(x => x.Name).ToList());
            }
            catch (Exception ex) {
                _logger.LogError($"Productbyfilter method, Error message {ex.Message}");
                ErrorViewModel err = new ErrorViewModel()
                {
                    RequestId = "Unable to filter the product"
                };
                return View("Error", err);
            }
        }

		#endregion

		#region Private members
		private ProductViewModel GetProductViewModel()
        {
            return new ProductViewModel()
            {
                Name = "",
                Category = this._categoryResource.Categories().Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                SubCategory = this._subCategoryResource.Subcategories().Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
        }

		#endregion
	}
}
