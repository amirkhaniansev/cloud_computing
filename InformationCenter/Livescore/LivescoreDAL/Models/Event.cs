using System;

namespace LivescoreDAL.Models
{
    public class Event : ModelBase
    {
        public long Id { get; set; }

        public int MatchId { get; set; }

        public int TeamId { get; set; }

        public short Type { get; set; }

        public string Decription { get; set; }

        public DateTime? Time { get; set; }
    }
}