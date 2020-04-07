using System.Threading.Tasks;
using AutoMapper;
using ManagementWebApi.Database;
using ManagementWebApi.DataModels;
using ManagementWebApi.DataModels.UpdateModels;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody]TaxIdUpdate upd, [FromServices] IMapper mapper)
        {
            var dbTaxId = await _db.TaxIds.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (dbTaxId == null)
            {
                return NotFound();
            }

            if (upd.SerialNumber != null && dbTaxId.SerialNumber != upd.SerialNumber)
            {
                dbTaxId.SerialNumber = upd.SerialNumber;
            }
            if (upd.ScanFileId != 0 && dbTaxId.ScanFileId != upd.ScanFileId)
            {
                dbTaxId.ScanFileId = upd.ScanFileId;
            }
            _db.TaxIds.Update(dbTaxId);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return Ok(mapper.Map<TaxId>(dbTaxId));
        }

        //[HttpPut]
        //public async Task<IActionResult> AddTaxId(TaxId tax)
        //{
        //    var dbTax = new DbTaxId()
        //    {
        //        SerialNumber = tax.SerialNumber,
        //        ScanFileId = tax.ScanFileId
        //    };
        //    _db.TaxIds.Add(dbTax);
        //    await _db.SaveChangesAsync().ConfigureAwait(false);
        //    return Ok(dbTax.ToModel());
        //}
    }
}