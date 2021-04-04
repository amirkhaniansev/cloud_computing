using InformationCenterUI.HttpClients;
using InformationCenterUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InformationCenterUI.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsClient client;

        public NewsController(NewsClient client)
        {
            this.client = client;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(News news)
        {
            await this.client.Post(news);
            return View("Success");
        }

        public IActionResult AddNews()
        {
            return View(new News());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            var newsColl = await client.Get();
            return View("NewsFeed", newsColl);
        }

        [HttpGet]
        public async Task<IActionResult> GetNews(int id)
        {
            var news = await client.Get(id);
            return View("NewsDetails", news);
        }
    }
}


