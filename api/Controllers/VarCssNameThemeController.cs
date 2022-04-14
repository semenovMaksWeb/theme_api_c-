using Microsoft.AspNetCore.Mvc;
using api.Server;
using api.Models.VarCssNameTheme;
using api.Models;
using api.Server.Theme;
using api.Server.VarCssName;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VarCssNameThemeController : ControllerBase
    {
        private VarCssNameThemeServer varCssNameThemeServer = new VarCssNameThemeServer();
        private ThemeServer themeServer = new ThemeServer();
        private VarCssNameServer varCssNameServer = new VarCssNameServer();
        private readonly ILogger<VarCssNameController> _logger;
        public VarCssNameThemeController(ILogger<VarCssNameController> logger)
        {
            _logger = logger;
        }
        [HttpGet("/var_css_name_theme/theme_id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<VarCssNameTheme>> GetAll(int id)
        {
            if (themeServer.checkIdRows(id))
            {
                return varCssNameThemeServer.getAll(id);
            }
            return NotFound(new Info("указанной темы не существует"));
        }



        [HttpGet("/var_css_name_theme/id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public VarCssNameTheme GetId(int id)
        {
            return varCssNameThemeServer.getId(id);
        }





        [HttpGet("/var_css_name_theme/get_insert")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<VarCssNameModel>> GetInsert(int id)
        {
            if (themeServer.checkIdRows(id)){
                return varCssNameThemeServer.getInsertVar(id);
            }
            return NotFound(new Info("указанной темы не существует"));
        }


        [HttpPut("/var_css_name_theme/update_all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Info UpdateAll(VarCssNameThemeUpdateAllList varCssNameThemeUpdateAll)
        {
            return varCssNameThemeServer.updateAll(varCssNameThemeUpdateAll);
        }

        [HttpDelete("/var_css_name_theme/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Info delete(int id)
        {
            return varCssNameThemeServer.delete(id);
        }

        [HttpPost("/var_css_name_theme/save")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<InfoAndId> save(VarCssNameThemeSave varCssNameThemeSave)
        {
            if (varCssNameServer.checkIdRows(varCssNameThemeSave.id_var))
            {
                return varCssNameThemeServer.save(varCssNameThemeSave);
            }
            Dictionary<string, string> errors_key = new Dictionary<string, string>();
            errors_key.Add("id", "переменная не существует");
            return BadRequest(libs.Libs.createCustomErrors(errors_key));
 
        }
    }
}
