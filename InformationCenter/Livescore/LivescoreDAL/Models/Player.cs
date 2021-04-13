using System;

namespace LivescoreDAL.Models
{
    public class Player : ModelBase
    {
        public int Id { get; set; }

        public DateTime Born { get; set; }

        public int TeamId { get; set; }

        public string Name { get; set; }

        public string PicURL { get; set; }
    }
}