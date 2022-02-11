namespace api.Models.Theme
{
    public class ThemeModel
    {
        public int? id { get; set; }

        public string? name { get; set; }
        public string? description { get; set; }

        public ThemeModel() { }
        public ThemeModel(int id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }
    }
}
