using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace GcpLogging.Controllers
{
    public class LoremIpsumController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoremIpsumController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Posts()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("fake-api");
            var result = await httpClient.GetStringAsync("posts");

            return RedirectToAction(nameof(CategoriesController.Index), "Categories");
        }
    }
}
