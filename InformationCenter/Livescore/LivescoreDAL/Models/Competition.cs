namespace LivescoreDAL.Models
{
    public class Competition : ModelBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SportId { get; set; }

        public int CountryId { get; set; }

        public string LogoURL { get; set; }
    }
}