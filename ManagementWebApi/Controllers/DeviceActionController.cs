using System;
using System.Threading.Tasks;
using ManagementWebApi.Database;
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
    public class DeviceActionController:ControllerBase
    {
        private readonly ManagementDbContext _db;

        public DeviceActionController(ManagementDbContext db)
        {
            _db = db;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DelAction(long id)
        {
            var dbAction = await _db.DeviceActions.FirstOrDefaultAsync(x => x.Id == id);
            if (dbAction == null)
            {
                return NotFound();
            }

            _db.DeviceActions.Remove(dbAction);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{id}/returnDate")]
        public async Task<IActionResult> UpdReturnDate(long id, [FromBody] DateTime date)
        {
            var dbAction = await _db.DeviceActions.FirstOrDefaultAsync(x => x.Id == id);
            if (dbAction == null)
            {
                return NotFound();
            }

            dbAction.ReturnDate = date;
            _db.DeviceActions.Update(dbAction);
            await _db.SaveChangesAsync();
            return Ok(dbAction.ToModel());
        }
    }
}