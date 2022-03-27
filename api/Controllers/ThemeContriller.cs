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
        [HttpGet("/theme/get_id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ThemeModel> getNameWhereId(int id)
        {
            ThemeModel themeModel = themeServer.getNameWhereId(id);
            if (themeModel.name == null)
            {
               return NotFound(new Info("тема не существует"));
            }
            return themeModel;
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
            return NotFound(new Info("тем не существует"));
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
            return NotFound(new Info("тем в текущем диапозоне не существует"));
        }


        [HttpPost("/theme/save")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<InfoAndId> Post(ThemeBodyModel themeBodyModel)
        {
            if(!themeServer.checkSave(themeBodyModel)) {
                return BadRequest(new Info("текущие имя уже занято"));
            }
           return themeServer.save(themeBodyModel);
        }
        
        [HttpDelete("/theme/delete")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Info> Delete (int id)
        {
            if (!themeServer.checkIdRows(id))
            {
                return NotFound(new Info("тема не найдена"));
            }
            themeServer.delete(id);
            return new Info("тема с id = " + id + " удачно удалена");
        }
        
        [HttpDelete("/theme/delete_in")]
        public Info DeleteIn(ThemeBodyModelDeleteIn ids)
        {
            themeServer.deleteIn(ids);
            return new Info("записи удачно удалены");
        }
        
        [HttpPut("/theme/update_all")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Info> UpdateAll(int id, ThemeBodyModelUpdateAll themeBodyModelUpdateAll)
        {
            if (!themeServer.checkIdRows(id)) {
                return NotFound(new Info("запись не найдена"));
            }
            if (!themeServer.checkUpdate(id, themeBodyModelUpdateAll))
            {
                return BadRequest(new Info("имя занято!"));
            }
            themeServer.updateAll(id, themeBodyModelUpdateAll);
            return new Info("запись удачно измененна");
        }
       
        [HttpPut("/theme/update_description")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Info> UpdateDescription(int id, ThemeBodyModelUpdateDescription themeBodyModelUpdateDescription)
        {
            if (!themeServer.checkIdRows(id))
            {
                return NotFound(new Info("запись не найдена"));
            }
            themeServer.updateDescription(id, themeBodyModelUpdateDescription);
            return new Info("запись удачно измененна");
        }

    }
}
