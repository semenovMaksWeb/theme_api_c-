using System.ComponentModel.DataAnnotations;
namespace api.Models
{
    public class InfoAndId: Info 
    {
        public InfoAndId(string info) : base(info)
        {
        }
        public int id { get; set; }
    }
}
