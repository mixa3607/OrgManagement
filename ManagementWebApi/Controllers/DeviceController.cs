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
    public class DeviceController:ControllerBase    
    {
        private readonly ManagementDbContext _db;

        public DeviceController(ManagementDbContext db)
        {
            _db = db;
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
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("software/{deviceId}")]
        public async Task<IActionResult> AddSoftware(long deviceId, [FromBody]Software soft)
        {
            var dbDevice = await _db.Devices.FirstOrDefaultAsync(x => x.Id == deviceId);
            if (dbDevice == null)
            {
                return NotFound();
            }

            var dbType = await _db.SoftwareTypes.FirstOrDefaultAsync(x => x.Name == soft.Type);
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

        [HttpPost("action/{deviceId}")]
        public async Task<IActionResult> AddAction(long deviceId, [FromBody]DeviceAction deviceAction)
        {
            var dbDevice = await _db.Devices.FirstOrDefaultAsync(x => x.Id == deviceId);
            if (dbDevice == null)
            {
                return NotFound();
            }

            var dbType = await _db.DeviceActionTypes.FirstOrDefaultAsync(x => x.Name == deviceAction.Type);
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