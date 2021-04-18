namespace LivescoreDAL.Models
{
    public class Country : ModelBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FlagURL { get; set; }
    }
}