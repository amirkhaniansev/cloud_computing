using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InformationCenter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InformationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrafficJamsController : ControllerBase
    {

        public static List<TrafficJam> TrafficJams { get; set; }

        public TrafficJamsController()
        {
            Location startLocation = default;
            startLocation.Lattitude = 10.07;
            startLocation.Longitude = 11.06;

            Location endLocation = default;
            endLocation.Lattitude = 12.07;
            endLocation.Longitude = 13.06;

            TrafficJams = new List<TrafficJam> { new TrafficJam() { Id = 2, Degree = 20, Street = "Կոմիտաս", StartLocation = startLocation, EndLocation = endLocation } };
        }

        [HttpGet]
        public List<TrafficJam> Get()
        {
            return TrafficJams;
        }

        [HttpGet("{id}")]
        public TrafficJam Get(int id)
        {
            return TrafficJams.Find(item => item.Id == id);
        }

        [HttpPost]
        public ActionResult<int> Post()
        {
            Location startLocation = default;
            startLocation.Lattitude = 10.07;
            startLocation.Longitude = 11.06;

            Location endLocation = default;
            endLocation.Lattitude = 12.07;
            endLocation.Longitude = 13.06;

            TrafficJams.Add(new TrafficJam() { Id = 3, Degree = 30, Street = "Բաղրամյան", StartLocation = startLocation, EndLocation = endLocation });

            return CreatedAtAction(nameof(Get), new { id = 3 }, 3);
        }
    }
}
