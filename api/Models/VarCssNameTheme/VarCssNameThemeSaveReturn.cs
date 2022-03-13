using System.ComponentModel.DataAnnotations;
namespace api.Models.VarCssNameTheme
{
    public class VarCssNameThemeSaveReturn: Info 
    {
        public VarCssNameThemeSaveReturn(string info) : base(info)
        {
        }
        public int id { get; set; }
    }
}
