using System.ComponentModel.DataAnnotations;
namespace api.Models.VarCssNameTheme
{
    public class VarCssNameThemeSave
    {
        [Required(ErrorMessage = "Укажите id переменной в теме")]
        public int id_var { get; set; }

        [Required(ErrorMessage = "Укажите id темы")]
        public int id_theme { get; set; }
    }
}
