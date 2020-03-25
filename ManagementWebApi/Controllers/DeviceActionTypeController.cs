using System;
using System.Linq;
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
    public class DeviceActionTypeController : ControllerBase
    {
        private readonly ManagementDbContext _db;

        public DeviceActionTypeController(ManagementDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int offset = 0, int count = int.MaxValue)
        {
            var dbDeviceActionTypes = await _db.DeviceActionTypes.Skip(offset).Take(count).ToArrayAsync();
            var total = await _db.Devices.CountAsync();
            return Ok(new GetAllResult<DeviceActionType>()
            {
                TotalCount = total,
                Values = dbDeviceActionTypes.Select(x => x.ToModel())
            });
        }

        [HttpPut]
        public async Task<IActionResult> Add([FromBody] string name)
        {
            if (!await _db.DeviceActionTypes.AnyAsync(x=>x.Name == name))
            {
                return Conflict("Type already created");
            }

            _db.DeviceActionTypes.Add(new DbDeviceActionType() {Name = name});
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Del(long id)
        {
            var dbType = await _db.DeviceActionTypes.Where(x => x.Id == id).Select(x=> new {type = x, total = x.NavDeviceActions.Count()}).FirstOrDefaultAsync();
            if (dbType.total != 0)
            {
                return Conflict();
            }
            if (dbType == null)
            {
                return NotFound();
            }

            _db.DeviceActionTypes.Remove(dbType.type);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}