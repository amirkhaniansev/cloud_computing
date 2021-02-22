using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationCenter.Models
{
    public struct Location
    {
        public double Lattitude { get; set; }

        public double Longitude { get; set; }
    }

    public class TrafficJam
    {
        public int Id { get; set; }

        public int Degree { get; set; }

        public string Street { get; set; }

        public Location StartLocation { get; set; }

        public Location EndLocation { get; set; }
    }
}
