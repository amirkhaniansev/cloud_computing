using InformationCenterUI.HttpClients;
using InformationCenterUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationCenterUI.Controllers
{
    public class TrafficJamsController : Controller
    {
        readonly TrafficJamClient client;
        public TrafficJamsController(TrafficJamClient client)
        {
            this.client = client;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddJam()
        {
            return View(new TrafficJam ());
        }
        [HttpPost]
        public async Task<IActionResult> Add(TrafficJam jam)
        {
            await this.client.PostTrafficJam(jam);
            return View("Success");
        }
        [HttpGet]
        public async Task<IActionResult> GetJams()
        {
            IEnumerable<TrafficJam> jams = await this.client.GetTrafficJams();
            return View("GetJams", jams);
        }
    }
}
