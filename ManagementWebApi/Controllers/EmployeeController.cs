using System.Linq;
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
    public class EmployeeController : ControllerBase
    {
        private readonly ManagementDbContext _db;

        public EmployeeController(ManagementDbContext db)
        {
            _db = db;
        }

        [HttpPost("cert/{userId}")]
        public async Task<IActionResult> AddCert(long userId, Cert cert)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == userId);
            if (employee == null)
            {
                return NotFound();
            }
            var dbCert = new DbCert()
            {
                NavEmployee = employee,
                CertFileId = cert.CertFile.Id,
                ContainerFileId = cert.ContainerFile.Id,
                Issuer = cert.Issuer,
                Name = cert.Name,
                NotAfter = cert.NotAfter.Value,
                NotBefore = cert.NotBefore.Value,
            };
            _db.Certs.Add(dbCert);
            await _db.SaveChangesAsync();
            return Ok(dbCert.ToModel());
        }

        [HttpPost("device/{userId}")]
        public async Task<IActionResult> AddDevice(long userId, [FromBody] Device device)
        {
            var devType = await _db.DeviceTypes.FirstOrDefaultAsync();
            if (devType == null)
            {
                ModelState.AddModelError("DeviceType", "Device type not found");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            if (!await _db.Employees.AnyAsync(x=>x.Id == userId))
            {
                ModelState.AddModelError("Employee", "Employee not found");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            var dbDevice = new DbDevice()
            {
                EmployeeId = userId,
                NavDeviceType = devType,
                InvNumber = device.InvNumber,
                Name = device.Name,
            };
            _db.Devices.Add(dbDevice);
            await _db.SaveChangesAsync();
            return Ok(dbDevice.ToModel());
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees(int count = 50, int offset = 0)
        {
            var totalCount = await _db.Employees.CountAsync();
            var values = await _db.Employees.Skip(offset).Take(count).ToArrayAsync();

            return Ok(new GetAllResult<Employee>()
            {
                TotalCount = totalCount,
                Values = values.Select(x => x.ToModel())
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(long id)
        {
            var dbEmployee = await _db.Employees
                .Include(x=>x.NavPassport)
                .Include(x=>x.NavTaxId)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (dbEmployee == null)
            {
                return NotFound();
            }

            return Ok(dbEmployee.ToModel());
        }

        //TODO: add ids to return value
        [HttpPut]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            var passport = employee.Passport;
            var dbPassport = new DbPassport()
            {
                Batch = passport.Batch,
                BirthDay = passport.BirthDay.Value,
                BirthPlace = passport.BirthPlace,
                Issuer = passport.Issuer,
                IssuedAt = passport.IssuedAt.Value,
                IssuerNum = passport.IssuerNum,
                RegPlace = passport.RegPlace,
                SerialNumber = passport.SerialNumber,
                ScanFileId = passport.ScanFileId
            };

            var taxId = employee.TaxId;
            var dbTaxId = new DbTaxId()
            {
                StrSerialNumber = taxId.SerialNumber,
                TaxIdScan = taxId.ScanFileId
            };

            var dbEmployee = new DbEmployee()
            {
                Initials = employee.Initials,
                Department = employee.Department,
                Email = employee.Email,
                DomainNameEntry = employee.DomainNameEntry,
                Ipv4StrAddress = employee.Ipv4Address,
                PhoneNumber = employee.PhoneNumber,
                WorkingPosition = employee.WorkingPosition,
                NavPassport = dbPassport,
                NavTaxId = dbTaxId
            };

            _db.Employees.Add(dbEmployee);
            await _db.SaveChangesAsync();
            return Ok(dbEmployee.ToModel());
        }


        //TODO: and del passport
        [HttpDelete("{id}")]
        public async Task<IActionResult> DelEmployee(long id)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateEmployee(long id,[FromBody]EmployeeUpdate upd)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            if (upd.Department != null && employee.Department != upd.Department)
            {
                employee.Department = upd.Department;
            }
            if (upd.Initials != null && employee.Initials != upd.Initials)
            {
                employee.Initials = upd.Initials;
            }
            if (upd.DomainNameEntry != null && employee.DomainNameEntry != upd.DomainNameEntry)
            {
                employee.DomainNameEntry = upd.DomainNameEntry;
            }
            if (upd.WorkingPosition != null && employee.WorkingPosition != upd.WorkingPosition)
            {
                employee.WorkingPosition = upd.WorkingPosition;
            }
            if (upd.PhoneNumber != null && employee.PhoneNumber != upd.PhoneNumber)
            {
                employee.PhoneNumber = upd.PhoneNumber;
            }
            if (upd.Email != null && employee.Email != upd.Email)
            {
                employee.Email = upd.Email;
            }
            if (upd.Ipv4Address != null && employee.Ipv4StrAddress != upd.Ipv4Address)
            {
                employee.Ipv4StrAddress = upd.Ipv4Address;
            }

            _db.Employees.Update(employee);
            await _db.SaveChangesAsync();
            return Ok(employee.ToModel());
        }
    }
}