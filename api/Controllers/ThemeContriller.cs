using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Server.Theme;
using api.Models.Theme;
namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ThemeController : ControllerBase
    {
        private ThemeServer themeServer = new ThemeServer();
        private readonly ILogger<ThemeController> _logger;
        public ThemeController(ILogger<ThemeController> logger)
        {
            _logger = logger;
        }
        [HttpGet("/theme/get_name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> getNameWhereId(int id)
        {
            string name = themeServer.getNameWhereId(id);
            if (name == "")
            {
                return NotFound("тема не существует");
            }
            return name;
        }

        [HttpGet("/theme/get_all")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ThemeModel>> GetAll()
        {
            List<ThemeModel> Theme = themeServer.getAll();
            if(Theme.Count > 0)
            {
                return Theme;
            }
            return NotFound("тем не существует");
        }

        [HttpGet("/theme/get_pages")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ThemeModel>> GetPages([FromQuery] Pagination pagination)
        {
            List<ThemeModel> Theme = themeServer.getPages(pagination);
            if (Theme.Count > 0)
            {
                return Theme;
            }
            return NotFound("тем в текущем диапозоне не существует");
        }


        [HttpPost("/theme/save")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> Post(ThemeBodyModel themeBodyModel)
        {
            if(!themeServer.checkSave(themeBodyModel)) {
                return BadRequest("текущие имя уже занято");
            }
            themeServer.save(themeBodyModel);
            return "запись удачно создана";
        }
        
        [HttpDelete("/theme/delete")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> Delete (int id)
        {
            if (!themeServer.checkIdRows(id))
            {
                return NotFound("запись не найдена");
            }
            themeServer.delete(id);
            return "запись удачно удалена";
        }
        
        [HttpDelete("/theme/delete_in")]
        public string DeleteIn(int[] ids)
        {
            themeServer.deleteIn(ids);
            return "записи удачно удалены";
        }
        
        [HttpPut("/theme/update_all")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> UpdateAll(int id, ThemeBodyModelUpdateAll themeBodyModelUpdateAll)
        {
            if (!themeServer.checkIdRows(id)) {
                return NotFound("запись не найдена");
            }
            themeServer.updateAll(id, themeBodyModelUpdateAll);
            return "запись удачно измененна";
        }
       
        [HttpPut("/theme/update_description")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> UpdateDescription(int id, ThemeBodyModelUpdateDescription themeBodyModelUpdateDescription)
        {
            if (!themeServer.checkIdRows(id))
            {
                return NotFound("запись не найдена");
            }
            themeServer.updateDescription(id, themeBodyModelUpdateDescription);
            return "запись удачно измененна";
        }

    }
}
