using System.ComponentModel.DataAnnotations;
namespace api.Models.Theme
{
    public class ThemeBodyModelUpdateDescription
    {
        [Required(ErrorMessage = "Укажите описание темы")]
        public string description { get; set; } = "";
    }
}
