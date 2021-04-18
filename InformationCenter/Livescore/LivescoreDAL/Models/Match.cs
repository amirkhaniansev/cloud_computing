using System;

namespace LivescoreDAL.Models
{
    public class Match : ModelBase
    {
        public long Id { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int SeasonId { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }
    }
}