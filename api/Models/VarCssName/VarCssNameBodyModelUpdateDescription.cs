using System.ComponentModel.DataAnnotations;
namespace api.Models
{
    public class VarCssNameBodyModelUpdateDescription
    {
        [Required(ErrorMessage = "Укажите описание переменной")]
        public string description { get; set; } = "";
    }
}
