using System.Linq;
using System.Threading.Tasks;
using ManagementWebApi.Database;
using ManagementWebApi.DataModels;
using ManagementWebApi.DataModels.DetailedModels;
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
    public class DeviceController:ControllerBase    
    {
        private readonly ManagementDbContext _db;

        public DeviceController(ManagementDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> GetAll(int offset = 0, int count = 50)
        {
            var dbDevices = await _db.Devices.Include(x => x.NavEmployee).Skip(offset).Take(count).ToArrayAsync();
            var total = await _db.Devices.CountAsync();
            return Ok(new GetAllResult<DeviceDt>()
            {
                TotalCount = total,
                Values = dbDevices.Select(x => x.ToModel())
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DelDevice(long id)
        {
            var dbDevice = await _db.Devices.FirstOrDefaultAsync(x => x.Id == id);
            if (dbDevice == null)
            {
                return NotFound();
            }

            _db.Devices.Remove(dbDevice);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }

        [HttpPost("{deviceId}/software")]
        public async Task<IActionResult> AddSoftware(long deviceId, [FromBody]Software soft)
        {
            var dbDevice = await _db.Devices.FirstOrDefaultAsync(x => x.Id == deviceId).ConfigureAwait(false);
            if (dbDevice == null)
            {
                return NotFound();
            }

            var dbType = await _db.SoftwareTypes.FirstOrDefaultAsync(x => x.Name == soft.Type).ConfigureAwait(false);
            if (dbType == null)
            {
                ModelState.AddModelError("Type", "Type not found");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var dbSoft = new DbSoftware()
            {
                Code = soft.Code,
                Name = soft.Name,
                NavType = dbType,
                NavDevice = dbDevice,
                
            };
            _db.Softwares.Add(dbSoft);
            await _db.SaveChangesAsync();
            return Ok(dbSoft.ToModel());
        }

        [HttpGet("{deviceId}/software")]
        public async Task<IActionResult> GetSoftwares(long deviceId, int offset = 0, int count = 40)
        {
            var dbSftw = await _db.Softwares.Skip(offset).Take(count).Where(x=>x.DeviceId == deviceId).ToArrayAsync().ConfigureAwait(false);
            var total = await _db.Softwares.Where(x => x.DeviceId == deviceId).CountAsync().ConfigureAwait(false);
            return Ok();
        }

        [HttpPost("{deviceId}/action")]
        public async Task<IActionResult> AddAction(long deviceId, [FromBody]DeviceAction deviceAction)
        {
            var dbDevice = await _db.Devices.FirstOrDefaultAsync(x => x.Id == deviceId);
            if (dbDevice == null)
            {
                return NotFound();
            }

            var dbType = await _db.DeviceActionTypes.FirstOrDefaultAsync(x => x.Id == deviceAction.TypeId);
            if (dbType == null)
            {
                ModelState.AddModelError("Type", "Type not found");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var dbDeviceAction = new DbDeviceAction()
            {
                NavActionType = dbType,
                NavDevice = dbDevice,
                ReceiptDate = deviceAction.ReceiptDate.Value,
                ReturnDate = deviceAction.ReturnDate
            };
            _db.DeviceActions.Add(dbDeviceAction);
            await _db.SaveChangesAsync();
            return Ok(dbDeviceAction.ToModel());
        }
    }
}