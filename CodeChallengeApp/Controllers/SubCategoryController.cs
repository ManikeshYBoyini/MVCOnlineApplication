using CodeChallengeApp.Interface;
using CodeChallengeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CodeChallengeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
		#region Private variables
		private readonly ISubCategoryResource subCategoryResource;
		private readonly ILogger<SubCategoryController> _logger;

		#endregion

		#region Constructor(s)
		public SubCategoryController(ISubCategoryResource subCategoryResource, ILogger<SubCategoryController> logger)
        {
            this.subCategoryResource = subCategoryResource;
			this._logger = logger;
		}

		#endregion

		#region Public members

        /// <summary>
        /// Fetch all sub categories
        /// </summary>
        /// <returns></returns>
		// GET: api/<SubCategoryController>
		[HttpGet]
        public IList<Subcategory> Get()
        {
            return this.subCategoryResource.Subcategories();
        }

        /// <summary>
        /// Fetch sub catergory by category id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSubCatByCatId")]
        public async Task<IList<Subcategory>> GetSubCatByCatId(int categoryId)
        {
			_logger.LogInformation("SubCategory/GetSubCatByCatId: request data: " + JsonConvert.SerializeObject(categoryId, Formatting.Indented));

			return await this.subCategoryResource.SubCatByCatId(categoryId);

            //Exception will be handled globally by CommonExceptionMiddleware 
        }

        // GET api/<SubCategoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SubCategoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SubCategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SubCategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

		#endregion
	}
}
