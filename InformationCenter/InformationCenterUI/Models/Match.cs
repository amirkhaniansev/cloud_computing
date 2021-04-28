using System;

namespace InformationCenterUI.Models
{
    public class Match
    {
        public long Id { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int SeasonId { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
