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

        public async Task<string> Posts()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("fake-api");
            return await httpClient.GetStringAsync("posts");
        }
    }
}
