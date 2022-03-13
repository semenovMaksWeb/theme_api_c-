using Microsoft.AspNetCore.Mvc;
using api.Server;
using api.Models.VarCssNameTheme;
namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VarCssNameThemeController : ControllerBase
    {
        private VarCssNameThemeServer varCssNameThemeServer = new VarCssNameThemeServer();
        private readonly ILogger<VarCssNameController> _logger;
        public VarCssNameThemeController(ILogger<VarCssNameController> logger)
        {
            _logger = logger;
        }
        [HttpGet("/var_css_name_theme/theme_id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public List<VarCssNameTheme> GetAll(int id)
        {
            return varCssNameThemeServer.getAll(id);
        }

        [HttpPut("/var_css_name_theme/update_all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public string UpdateAll(List<VarCssNameThemeUpdateAll> varCssNameThemeUpdateAll)
        {
            return varCssNameThemeServer.updateAll(varCssNameThemeUpdateAll);
        }

        [HttpDelete("/var_css_name_theme/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public string delete(int id)
        {
            return varCssNameThemeServer.delete(id);
        }

        [HttpPost("/var_css_name_theme/save")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public string save(VarCssNameThemeSave varCssNameThemeSave)
        {
            return varCssNameThemeServer.save(varCssNameThemeSave);
        }
    }
}
