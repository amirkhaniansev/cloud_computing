using InformationCenterUI.HttpClients;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationCenterUI.Controllers
{
    public class MatchesController : Controller
    {
        private readonly LivescoreGraphQLClient client;

        public MatchesController(LivescoreGraphQLClient client)
        {
            this.client = client;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetMatches()
        {
            return this.View("GetMatches", (await this.client.GetMatches()).Matches);
        }
    }
}
