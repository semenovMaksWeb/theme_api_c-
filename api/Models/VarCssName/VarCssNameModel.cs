namespace api.Models
{
    public class VarCssNameModel
    {
        public int? id { get; set;}
        public string? name { get; set; }
        public string? description { get; set; }
        public VarCssNameModel() { }
        public VarCssNameModel(int id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }
        
    }
}
