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
    public class PassportController : ControllerBase
    {
        private readonly ManagementDbContext _db;

        public PassportController(ManagementDbContext db)
        {
            _db = db;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DelPassport(long id)
        {
            var passport = await _db.Passports.FirstOrDefaultAsync(x => x.Id == id);
            if (passport == null)
            {
                return NotFound();
            }

            _db.Passports.Remove(passport);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdPassport(long id, [FromBody]PassportUpdate upd, [FromServices] IMapper mapper)
        {
            var dbPassport = await _db.Passports.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (dbPassport == null)
            {
                return NotFound();
            }

            if (upd.Initials != null && dbPassport.Initials != upd.Initials)
            {
                dbPassport.Initials = upd.Initials;
            }
            if (upd.Issuer != null && dbPassport.Issuer != upd.Issuer)
            {
                dbPassport.Issuer = upd.Issuer;
            }
            if (upd.RegPlace != null && dbPassport.RegPlace != upd.RegPlace)
            {
                dbPassport.RegPlace = upd.RegPlace;
            }
            if (upd.BirthPlace != null && dbPassport.BirthPlace != upd.BirthPlace)
            {
                dbPassport.BirthPlace = upd.BirthPlace;
            }
            if (upd.SerialNumber != 0 && dbPassport.SerialNumber != upd.SerialNumber)
            {
                dbPassport.SerialNumber = upd.SerialNumber;
            }
            if (upd.Batch != 0 && dbPassport.Batch != upd.Batch)
            {
                dbPassport.Batch = upd.Batch;
            }
            if (upd.IssuerNum != 0 && dbPassport.IssuerNum != upd.IssuerNum)
            {
                dbPassport.IssuerNum = upd.IssuerNum;
            }
            if (upd.IssuedAt != null && dbPassport.IssuedAt != upd.IssuedAt)
            {
                dbPassport.IssuedAt = upd.IssuedAt.Value;
            }
            if (upd.BirthDay != null && dbPassport.BirthDay != upd.BirthDay)
            {
                dbPassport.BirthDay = upd.BirthDay.Value;
            }
            if (upd.ScanFileId != 0 && dbPassport.ScanFileId != upd.ScanFileId)
            {
                dbPassport.ScanFileId = upd.ScanFileId;
            }

            _db.Passports.Update(dbPassport);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return Ok(mapper.Map<Passport>(dbPassport));
        }
    }
}