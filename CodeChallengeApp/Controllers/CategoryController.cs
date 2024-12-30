using CodeChallengeApp.Interface;
using CodeChallengeApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallengeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
		#region Private variables
		private readonly ICategoryResource categoryResource;

		#endregion

		#region Constructors
		public CategoryController(ICategoryResource categoryResource)
        {
            this.categoryResource = categoryResource;
        }

		#endregion

		#region Public members

        /// <summary>
        /// Fetch all categories
        /// </summary>
        /// <returns></returns>
		// GET: api/<CategoryController>
		[HttpGet]
        public IList<Category> Get()
        {
            return this.categoryResource.Categories();

			//Exception will be handled globally by CommonExceptionMiddleware 
		}

		// GET api/<CategoryController>/5
		[HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

		#endregion
	}
}
