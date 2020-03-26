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
    public class DeviceTypeController: ControllerBase
    {
        private readonly ManagementDbContext _db;

        public DeviceTypeController(ManagementDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int offset = 0, int count = int.MaxValue)
        {
            var dbDeviceTypes = await _db.DeviceTypes.Skip(offset).Take(count).ToArrayAsync();
            var total = await _db.Devices.CountAsync();
            return Ok(new GetAllResult<DeviceType>()
            {
                TotalCount = total,
                Values = dbDeviceTypes.Select(x => x.ToModel())
            });
        }

        [HttpPut]
        public async Task<IActionResult> Add([FromBody] string name)
        {
            if (await _db.DeviceTypes.AnyAsync(x => x.Name == name))
            {
                return Conflict("Type already created");
            }

            _db.DeviceTypes.Add(new DbDeviceType() { Name = name });
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Del(long id)
        {
            var dbType = await _db.DeviceTypes.Where(x => x.Id == id).Select(x => new { type = x, total = x.NavDevices.Count() }).FirstOrDefaultAsync();
            if (dbType.total != 0)
            {
                return Conflict();
            }
            if (dbType == null)
            {
                return NotFound();
            }

            _db.DeviceTypes.Remove(dbType.type);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}