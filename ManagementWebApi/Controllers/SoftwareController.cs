using System.Threading.Tasks;
using ManagementWebApi.Database;
using ManagementWebApi.DataModels;
using ManagementWebApi.Extensions;
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
    public class SoftwareController: ControllerBase
    {
        private readonly ManagementDbContext _db;

        public SoftwareController(ManagementDbContext db)
        {
            _db = db;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DelSoft(long id)
        {
            var dbSoftware = await _db.Softwares.FirstOrDefaultAsync(x => x.Id == id);
            if (dbSoftware == null)
            {
                return NotFound();
            }

            _db.Softwares.Remove(dbSoftware);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}