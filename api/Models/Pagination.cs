using System.ComponentModel.DataAnnotations;
namespace api.Models
{
    public class Pagination
    {
        [Range(0,int.MaxValue)]
        public int limit { set; get; }

        [Range(0, int.MaxValue)]
        public int offset { set; get; }
    }
}
