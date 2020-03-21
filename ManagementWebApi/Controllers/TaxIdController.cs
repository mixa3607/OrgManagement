using System.Threading.Tasks;
using ManagementWebApi.Database;
using ManagementWebApi.DataModels;
using ManagementWebApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using WebApiSharedParts.Attributes;
using WebApiSharedParts.Enums;

namespace ManagementWebApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [AuthorizeEnum(EUserRole.Admin)]
    [Route(GlobalConst.ApiRoot + "/[controller]")]
    public class TaxIdController: ControllerBase
    {
        private readonly ManagementDbContext _db;

        public TaxIdController(ManagementDbContext db)
        {
            _db = db;
        }

        [HttpPut]
        public async Task<IActionResult> AddTaxId(TaxId tax)
        {
            var dbTax = new DbTaxId()
            {
                StrSerialNumber = tax.SerialNumber,
                TaxIdScan = tax.ScanFileId
            };
            _db.TaxIds.Add(dbTax);
            await _db.SaveChangesAsync();
            return Ok(dbTax.ToModel());
        }
    }
}