using System.ComponentModel.DataAnnotations;
namespace api.Models.VarCssNameTheme
{
    public class VarCssNameThemeUpdateAll
    {
        [Required(ErrorMessage = "Укажите id переменной в теме")]
        public int id { get; set; }
        [Required(ErrorMessage = "Укажите value переменной в теме")]
        public string value { get; set; } = "";
    }
}
