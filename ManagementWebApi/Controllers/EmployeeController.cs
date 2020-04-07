using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ManagementWebApi.Database;
using ManagementWebApi.DataModels;
using ManagementWebApi.DataModels.DetailedModels;
using ManagementWebApi.DataModels.ListModels;
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
    public class EmployeeController : ControllerBase
    {
        private readonly ManagementDbContext _db;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperCfg;

        public EmployeeController(ManagementDbContext db, IMapper mapper, MapperConfiguration mapperCfg)
        {
            _db = db;
            _mapper = mapper;
            _mapperCfg = mapperCfg;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees(int count = 50, int offset = 0)
        {
            var totalCount = await _db.Employees.CountAsync().ConfigureAwait(false);
            var values = await _db.Employees
                .Skip(offset).Take(count)
                .ProjectTo<EmployeeL>(_mapperCfg)
                .ToArrayAsync().ConfigureAwait(false);

            return Ok(new GetAllResult<EmployeeL>()
            {
                TotalCount = totalCount,
                Values = values
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(long id)
        {
            var employee = await _db.Employees
                .ProjectTo<EmployeeDt>(_mapperCfg)
                .FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDt employee)
        {
            employee.Id = 0;
            employee.Passport.Id = 0;
            employee.TaxId.Id = 0;

            var dbEmployee = _mapper.Map<DbEmployee>(employee);
            _db.Employees.Add(dbEmployee);
            await _db.SaveChangesAsync().ConfigureAwait(false);

            return Ok(_mapper.Map<EmployeeDt>(dbEmployee));
        }

        //TODO: and del passport
        [HttpDelete("{id}")]
        public async Task<IActionResult> DelEmployee(long id)
        {
            var employeeId = await _db.Employees.Select(x => x.Id).FirstOrDefaultAsync(x => x == id)
                .ConfigureAwait(false);
            if (employeeId == 0)
            {
                return NotFound();
            }

            _db.Employees.Remove(new DbEmployee() {Id = employeeId});
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Find([FromQuery] string query = "", int offset = 0, int count = 10)
        {
            var employees = await _db.Employees
                .Where(x => EF.Functions.Like(x.Name, query + '%'))
                .Skip(offset)
                .Take(count)
                .ProjectTo<IdNamePair>(_mapperCfg)
                .ToArrayAsync().ConfigureAwait(false);
            return Ok(employees);
        }

        [HttpPost("{userId}/cert")]
        public async Task<IActionResult> AddCert(long userId, CertDt cert)
        {
            var employeeId = await _db.Employees.AnyAsync(x => x.Id == userId).ConfigureAwait(false);
            if (!employeeId)
            {
                return NotFound();
            }

            var dbCert = _mapper.Map<DbCert>(cert);
            _db.Certs.Add(dbCert);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return Ok(_mapper.Map<CertDt>(dbCert));
        }

        [HttpPost("{userId}/device")]
        public async Task<IActionResult> AddDevice(long userId, [FromBody] DeviceDt device)
        {
            var devTypeId = await _db.DeviceTypes.Select(x => x.Id)
                .FirstOrDefaultAsync(x => x == device.TypeId).ConfigureAwait(false);

            //TODO: move checks to fluent validation
            if (devTypeId == 0)
            {
                ModelState.AddModelError("DeviceType", "Device type not found");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            if (!await _db.Employees.AnyAsync(x => x.Id == userId).ConfigureAwait(false))
            {
                ModelState.AddModelError("Employee", "Employee not found");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var dbDevice = _mapper.Map<DbDevice>(device);
            dbDevice.Id = 0;
            _db.Devices.Add(dbDevice);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return Ok(_mapper.Map<DeviceDt>(dbDevice));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(long id, [FromBody] EmployeeUpdate upd)
        {
            var dbEmployee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (dbEmployee == null)
            {
                return NotFound();
            }

            if (upd.Department != null && dbEmployee.Department != upd.Department)
            {
                dbEmployee.Department = upd.Department;
            }

            if (upd.Name != null && dbEmployee.Name != upd.Name)
            {
                dbEmployee.Name = upd.Name;
            }

            if (upd.DomainNameEntry != null && dbEmployee.DomainNameEntry != upd.DomainNameEntry)
            {
                dbEmployee.DomainNameEntry = upd.DomainNameEntry;
            }

            if (upd.WorkingPosition != null && dbEmployee.WorkingPosition != upd.WorkingPosition)
            {
                dbEmployee.WorkingPosition = upd.WorkingPosition;
            }

            if (upd.PhoneNumber != null && dbEmployee.PhoneNumber != upd.PhoneNumber)
            {
                dbEmployee.PhoneNumber = upd.PhoneNumber;
            }

            if (upd.Email != null && dbEmployee.Email != upd.Email)
            {
                dbEmployee.Email = upd.Email;
            }

            if (upd.Ipv4Address != null && dbEmployee.Ipv4Address != upd.Ipv4Address)
            {
                dbEmployee.Ipv4Address = upd.Ipv4Address;
            }

            _db.Employees.Update(dbEmployee);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return Ok(_mapper.Map<EmployeeDt>(dbEmployee));
        }
    }
}