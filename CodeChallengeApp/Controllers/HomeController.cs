using CodeChallengeApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodeChallengeApp.Controllers
{
    public class HomeController : Controller
    {
		#region Private variables
		private readonly ILogger<HomeController> _logger;

        #endregion

        #region Constructor(s)
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

		#endregion

		#region Public members

        /// <summary>
        /// Inital page load of the application
        /// </summary>
        /// <returns></returns>
		public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Loads privacy 
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Specific error page for all exceptions
        /// </summary>
        /// <returns></returns>
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogInformation(string.Empty);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Generic error page for exceptions
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult CustomError()
        {
            return View();
        }
		#endregion
	}
}