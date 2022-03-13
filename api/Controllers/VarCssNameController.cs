using Microsoft.AspNetCore.Mvc;
using api.Server.VarCssName;
using api.Models;
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
        public ActionResult<Info> Post(VarCssNameBodyModel varCssNameBodyModel)
        {
            if(!varCssNameServer.checkSave(varCssNameBodyModel)) {
                return BadRequest(new Info("текущие имя уже занято"));
            }
            varCssNameServer.save(varCssNameBodyModel);
            return new Info("запись удачно создана");
        }
        
        [HttpDelete("/var_css_name/delete")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> Delete (int id)
        {
            if (!varCssNameServer.checkIdRows(id))
            {
                return NotFound("запись не найдена");
            }
            varCssNameServer.delete(id);
            return "запись удачно удалена";
        }
        
        [HttpDelete("/var_css_name/delete_in")]
        public Info DeleteIn(int[] ids)
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
