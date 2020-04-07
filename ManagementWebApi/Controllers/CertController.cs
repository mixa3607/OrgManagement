using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ManagementWebApi.Database;
using ManagementWebApi.DataModels;
using ManagementWebApi.DataModels.ListModels;
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
    public class CertController : ControllerBase
    {
        private readonly ManagementDbContext _db;

        private readonly MapperConfiguration _mapperCfg;

        public CertController(ManagementDbContext db, MapperConfiguration mapperCfg)
        {
            _db = db;
            _mapperCfg = mapperCfg;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int offset = 0, int count = 50)
        {
            var certs = await _db.Certs.ProjectTo<CertL>(_mapperCfg).Skip(offset).Take(count).ToArrayAsync().ConfigureAwait(false);
            var total = await _db.Devices.CountAsync().ConfigureAwait(false);
            return Ok(new GetAllResult<CertL>()
            {
                TotalCount = total,
                Values = certs
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Del(long id)
        {
            var dbCert = await _db.Certs.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (dbCert == null)
            {
                return NotFound();
            }

            _db.Certs.Remove(dbCert);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }
    }
}