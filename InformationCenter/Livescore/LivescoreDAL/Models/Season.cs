using System;

namespace LivescoreDAL.Models
{
    public class Season : ModelBase
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Description { get; set; }

        public int CompetitionId { get; set; }
    }
}
