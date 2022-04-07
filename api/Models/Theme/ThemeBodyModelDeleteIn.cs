using System.ComponentModel.DataAnnotations;

namespace api.Models.Theme
{
    public class ThemeBodyModelDeleteIn
    {
        [Required(ErrorMessage = "Укажите массив id")]
        public int[] id { get; set; }
 
    }
}
