using System.Threading.Tasks;
using ManagementWebApi.Database;
using ManagementWebApi.DataModels;
using ManagementWebApi.DataModels.UpdateModels;
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
    public class TaxIdController: ControllerBase
    {
        private readonly ManagementDbContext _db;

        public TaxIdController(ManagementDbContext db)
        {
            _db = db;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody]TaxIdUpdate upd)
        {
            var dbTaxId = await _db.TaxIds.FirstOrDefaultAsync(x => x.Id == id);
            if (dbTaxId == null)
            {
                return NotFound();
            }

            if (upd.SerialNumber != null && dbTaxId.StrSerialNumber != upd.SerialNumber)
            {
                dbTaxId.StrSerialNumber = upd.SerialNumber;
            }
            if (upd.ScanFileId != 0 && dbTaxId.TaxIdScan != upd.ScanFileId)
            {
                dbTaxId.TaxIdScan = upd.ScanFileId;
            }
            _db.TaxIds.Update(dbTaxId);
            await _db.SaveChangesAsync();
            return Ok(dbTaxId.ToModel());
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