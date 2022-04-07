using Microsoft.AspNetCore.Mvc;
using api.Server.VarCssName;
using api.Models;
using api.Models.Theme;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VarCssNameController: ControllerBase
    {
        private VarCssNameServer varCssNameServer = new VarCssNameServer();
        private readonly ILogger<VarCssNameController> _logger;
        public VarCssNameController(ILogger<VarCssNameController> logger)
        {
            _logger = logger;
        }



        [HttpGet("/var_css_name/get_id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VarCssNameModel> getNameWhereId(int id)
        {
            VarCssNameModel varCssNameModel = varCssNameServer.getWhereId(id);
            if (varCssNameModel.name == null)
            {
                return NotFound(new Info("переменная не существует"));
            }
            return varCssNameModel;
        }



        [HttpGet("/var_css_name/get_all")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<VarCssNameModel>> GetAll()
        {
            List<VarCssNameModel> varCssName = varCssNameServer.getAll();
            if(varCssName.Count > 0)
            {
                return varCssName;
            }
            return NotFound(new Info("переменных не существует"));
        }

        [HttpPost("/var_css_name/save")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<InfoAndId> Post(VarCssNameBodyModel varCssNameBodyModel)
        {
            if(!varCssNameServer.checkSave(varCssNameBodyModel)) {
                Dictionary<string, string> errors_key = new Dictionary<string, string>();
                errors_key.Add("name", "текущие имя переменной уже занято");
                return BadRequest(libs.Libs.createCustomErrors(errors_key));
            }
            InfoAndId infoAndId = new InfoAndId("переменная удачно создана");
            infoAndId.id = varCssNameServer.save(varCssNameBodyModel);
            return infoAndId;
        }
        
        [HttpDelete("/var_css_name/delete")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Info> Delete (int id)
        {
            if (!varCssNameServer.checkIdRows(id))
            {
                return NotFound(new Info("переменная не найдена"));
            }
            varCssNameServer.delete(id);
            return new Info("переменная удачно удалена");
        }
        
        [HttpDelete("/var_css_name/delete_in")]
        public Info DeleteIn(ThemeBodyModelDeleteIn ids)
        {
            varCssNameServer.deleteIn(ids);
            return new Info("записи удачно удалены");
        }
        
        [HttpPut("/var_css_name/update_all")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Info> UpdateAll(int id, VarCssNameBodyModelUpdateAll varCssNameBodyModelUpdateAll)
        {
            if (!varCssNameServer.checkIdRows(id)) {
                return NotFound(new Info("запись не найдена"));
            }
            if (!varCssNameServer.checkUpdate(id, varCssNameBodyModelUpdateAll))
            {
                Dictionary<string, string> errors_key = new Dictionary<string, string>();
                errors_key.Add("name", "текущие имя пеменнной уже занято");
                return BadRequest(libs.Libs.createCustomErrors(errors_key));
            }
            varCssNameServer.updateAll(id, varCssNameBodyModelUpdateAll);
            return  new Info("запись удачно измененна");
        }
       
        [HttpPut("/var_css_name/update_description")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Info> UpdateDescription(int id, VarCssNameBodyModelUpdateDescription varCssNameBodyModelUpdateDescription)
        {
            if (!varCssNameServer.checkIdRows(id))
            {
                return NotFound(new Info("запись не найдена"));
            }
            varCssNameServer.updateDescription(id, varCssNameBodyModelUpdateDescription);
            return new Info("запись удачно измененна");
        }

    }
}
