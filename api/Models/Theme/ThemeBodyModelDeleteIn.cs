using System.ComponentModel.DataAnnotations;

namespace api.Models.Theme
{
    public class ThemeBodyModelDeleteIn
    {
        [Required(ErrorMessage = "Укажите массив id тем")]
        public int[] id { get; set; }
 
    }
}
