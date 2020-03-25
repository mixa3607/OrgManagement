using System.Threading.Tasks;
using ManagementWebApi.Database;
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

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdPassport(long id, [FromBody]PassportUpdate upd)
        {
            var passport = await _db.Passports.FirstOrDefaultAsync(x => x.Id == id);
            if (passport == null)
            {
                return NotFound();
            }

            if (upd.Initials != null && passport.Initials != upd.Initials)
            {
                passport.Initials = upd.Initials;
            }
            if (upd.Issuer != null && passport.Issuer != upd.Issuer)
            {
                passport.Issuer = upd.Issuer;
            }
            if (upd.RegPlace != null && passport.RegPlace != upd.RegPlace)
            {
                passport.RegPlace = upd.RegPlace;
            }
            if (upd.BirthPlace != null && passport.BirthPlace != upd.BirthPlace)
            {
                passport.BirthPlace = upd.BirthPlace;
            }
            if (upd.SerialNumber != 0 && passport.SerialNumber != upd.SerialNumber)
            {
                passport.SerialNumber = upd.SerialNumber;
            }
            if (upd.Batch != 0 && passport.Batch != upd.Batch)
            {
                passport.Batch = upd.Batch;
            }
            if (upd.IssuerNum != 0 && passport.IssuerNum != upd.IssuerNum)
            {
                passport.IssuerNum = upd.IssuerNum;
            }
            if (upd.IssuedAt != null && passport.IssuedAt != upd.IssuedAt)
            {
                passport.IssuedAt = upd.IssuedAt;
            }
            if (upd.BirthDay != null && passport.BirthDay != upd.BirthDay)
            {
                passport.BirthDay = upd.BirthDay;
            }
            if (upd.ScanFileId != 0 && passport.ScanFileId != upd.ScanFileId)
            {
                passport.ScanFileId = upd.ScanFileId;
            }

            _db.Passports.Update(passport);
            await _db.SaveChangesAsync();
            return Ok(passport.ToModel());
        }
    }
}