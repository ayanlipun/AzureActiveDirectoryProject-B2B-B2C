using AzureADB2CWeb.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AzureADB2CWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SignIn()
        {
            var scheme = OpenIdConnectDefaults.AuthenticationScheme;
            var redirectUrl = Url.ActionContext.HttpContext.Request.Scheme + "://" + Url.ActionContext.HttpContext.Request.Host;
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = redirectUrl
            }, scheme);
        }

        public IActionResult SignOut()
        {
            var scheme = OpenIdConnectDefaults.AuthenticationScheme;
            return SignOut(new AuthenticationProperties(), CookieAuthenticationDefaults.AuthenticationScheme, scheme);
        }

        public async Task<IActionResult> GetApiCall()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7224/WeatherForecast");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, accessToken);
            var response = await client.SendAsync(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                // issue
            }
            return Content(response.ToString());
        }
    }
}