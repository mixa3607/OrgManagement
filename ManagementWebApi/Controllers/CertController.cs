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
    public class CertController : ControllerBase
    {
        private readonly ManagementDbContext _db;

        public CertController(ManagementDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int offset = 0, int count = 50)
        {
            var dbCerts = await _db.Certs.Include(x => x.NavEmployee).Skip(offset).Take(count).ToArrayAsync();
            var total = await _db.Devices.CountAsync();
            return Ok(new GetAllResult<Cert>()
            {
                TotalCount = total,
                Values = dbCerts.Select(x => x.ToModel())
            });
        }
    }
}