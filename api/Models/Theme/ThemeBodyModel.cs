using System.ComponentModel.DataAnnotations;

namespace api.Models.Theme
{
    public class ThemeBodyModel
    {
        [Required(ErrorMessage = "Укажите имя темы")]
        public string name { get; set; } = "";
        public string description { get; set; } = "";
    }
}
