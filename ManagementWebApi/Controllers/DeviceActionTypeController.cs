﻿using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class DeviceActionTypeController : ControllerBase
    {
        private readonly ManagementDbContext _db;
        private readonly MapperConfiguration _mapperCfg;

        public DeviceActionTypeController(ManagementDbContext db, MapperConfiguration mapperCfg)
        {
            _db = db;
            _mapperCfg = mapperCfg;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int offset = 0, int count = int.MaxValue)
        {
            var types = await _db.DeviceActionTypes
                .Skip(offset)
                .Take(count)
                .ProjectTo<IdNamePair>(_mapperCfg)
                .ToArrayAsync().ConfigureAwait(false);

            var total = await _db.Devices.CountAsync().ConfigureAwait(false);
            return Ok(new GetAllResult<IdNamePair>()
            {
                TotalCount = total,
                Values = types
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] string name)
        {
            if (await _db.DeviceActionTypes.AnyAsync(x => x.Name == name).ConfigureAwait(false))
            {
                return Conflict("Type already created");
            }

            _db.DeviceActionTypes.Add(new DbDeviceActionType() { Name = name });
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Del(long id)
        {
            var dbType = await _db.DeviceActionTypes
                .Where(x => x.Id == id)
                .Select(x => new { type = x, any = x.NavDeviceActions.Any() })
                .FirstOrDefaultAsync().ConfigureAwait(false);

            if (dbType == null)
            {
                return NotFound();
            }
            if (dbType.any)
            {
                return Conflict();
            }

            _db.DeviceActionTypes.Remove(dbType.type);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }
    }
}