using System.ComponentModel.DataAnnotations;
namespace api.Models
{
    public class VarCssNameBodyModel
    {
        [Required(ErrorMessage = "Укажите имя переменной")]
        public string name { get; set; } = "";
        public string description { get; set; } = "";
    }
}
