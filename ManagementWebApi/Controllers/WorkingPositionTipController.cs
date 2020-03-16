using System.Linq;
using System.Threading.Tasks;
using ManagementWebApi.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSharedParts.Attributes;
using WebApiSharedParts.Enums;

namespace ManagementWebApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [AuthorizeEnum(EUserRole.Admin)]
    [Route(GlobalConst.ApiRoot + "/[controller]")]
    public class WorkingPositionTipController : ControllerBase
    {
        private readonly ManagementDbContext _db;

        public WorkingPositionTipController(ManagementDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> GetAll()
        {
            return Ok(await _db.WorkingPositionHelpers.Select(x => x.Name).ToArrayAsync());
        }
    }
}