using System;
using System.Threading.Tasks;
using AutoMapper;
using ManagementWebApi.Database;
using ManagementWebApi.DataModels;
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
        private readonly IMapper _mapper;

        public DeviceActionController(ManagementDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DelAction(long id)
        {
            var dbAction = await _db.DeviceActions.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (dbAction == null)
            {
                return NotFound();
            }

            _db.DeviceActions.Remove(dbAction);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }

        [HttpPut("{id}/returnDate")]
        public async Task<IActionResult> UpdReturnDate(long id, [FromBody] DateTime date)
        {
            var dbAction = await _db.DeviceActions.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (dbAction == null)
            {
                return NotFound();
            }

            dbAction.ReturnDate = date;
            _db.DeviceActions.Update(dbAction);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return Ok(_mapper.Map<DeviceAction>(dbAction));
        }
    }
}