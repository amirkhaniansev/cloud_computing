using System;

namespace LivescoreDAL.Models
{
    public class Team : ModelBase
    {
        public int Id { get; set; }

        public DateTime Founded { get; set; }

        public string Name { get; set; }

        public string Stadium { get; set; }

        public string LogoURL { get; set; }

        public string StadiumPicURL { get; set; }

        public int CountryId { get; set;  }

        public int SportId { get; set; }
    }
}