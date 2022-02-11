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
            return NotFound("переменных не существует");
        }

        [HttpPost("/var_css_name/save")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> Post(VarCssNameBodyModel varCssNameBodyModel)
        {
            if(!varCssNameServer.checkSave(varCssNameBodyModel)) {
                return BadRequest("текущие имя уже занято");
            }
            varCssNameServer.save(varCssNameBodyModel);
            return "запись удачно создана";
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
        public string DeleteIn(int[] ids)
        {
            varCssNameServer.deleteIn(ids);
            return "записи удачно удалены";
        }
        
        [HttpPut("/var_css_name/update_all")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> UpdateAll(int id, VarCssNameBodyModelUpdateAll varCssNameBodyModelUpdateAll)
        {
            if (!varCssNameServer.checkIdRows(id)) {
                return NotFound("запись не найдена");
            }
            varCssNameServer.updateAll(id, varCssNameBodyModelUpdateAll);
            return "запись удачно измененна";
        }
       
        [HttpPut("/var_css_name/update_description")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> UpdateDescription(int id, VarCssNameBodyModelUpdateDescription varCssNameBodyModelUpdateDescription)
        {
            if (!varCssNameServer.checkIdRows(id))
            {
                return NotFound("запись не найдена");
            }
            varCssNameServer.updateDescription(id, varCssNameBodyModelUpdateDescription);
            return "запись удачно измененна";
        }

    }
}
