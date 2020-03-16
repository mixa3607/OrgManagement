using System;
using System.IO;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ManagementWebApi.Database;
using ManagementWebApi.DataModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSharedParts.Attributes;
using WebApiSharedParts.Enums;
using WebApiSharedParts.Extensions;

namespace ManagementWebApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [AuthorizeEnum(EUserRole.Admin)]
    [Route(GlobalConst.ApiRoot + "/[controller]")]
    public class FileController : Controller
    {
        private readonly ManagementDbContext _db;
        private readonly string _virtualRoot;

        public FileController(ManagementDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _virtualRoot = env.WebRootPath;
        }

        [HttpGet("{md5}")]
        public async Task<IActionResult> GetAsync(string md5)
        {
            if (md5.Length != 32)
            {
                ModelState.AddModelError("Md5", "Invalid md5 string");
                return BadRequest(ModelState);
            }

            md5 = md5.ToLower();
            var prefix = md5.Substring(0, 3);
            var virtualFilePath = Path.Combine(GlobalConst.RelFilesPath, prefix, md5);
            var dbFile = await _db.Files.FirstOrDefaultAsync(x => x.Md5Hash == md5);

            var returnFileName = virtualFilePath;
            switch (dbFile.Type)
            {
                case EFileType.Binary: returnFileName += ".bin"; break;
                case EFileType.Image: returnFileName += ".png"; break;
                case EFileType.Cert: returnFileName += ".crt"; break;
            }

            if (!System.IO.File.Exists(Path.Combine(_virtualRoot, virtualFilePath)))
            {
                return NotFound(new ProblemDetails()
                {
                    Status = StatusCodes.Status404NotFound,
                    Detail = $"File with hash {md5} not found",
                    Title = "File Not Found"
                });
            }

            return File(virtualFilePath, MediaTypeNames.Application.Octet, returnFileName);
        }

        #region TODO: need refactoring
        [HttpPut("cert")]
        public async Task<IActionResult> UploadCert(IFormFile file)
        {
            var fileStream = file.OpenReadStream();
            var md5Hasher = MD5.Create();
            var hash = md5Hasher.ComputeHash(fileStream).ToHexStr();
            var prefix = hash.Substring(0, 3);
            var storeDir = Path.Combine(_virtualRoot, GlobalConst.RelFilesPath, prefix);
            if (!Directory.Exists(storeDir))
                Directory.CreateDirectory(storeDir);

            var certBytes = new byte[fileStream.Length];
            fileStream.Position = 0;
            fileStream.Read(certBytes, 0, (int)fileStream.Length);
            var cert = new X509Certificate2(certBytes);
            
            var dbFile = new DbFile()
            {
                CreateDate = DateTime.UtcNow,
                Md5Hash = hash,
                Type = EFileType.Cert
            };
            _db.Files.Add(dbFile);
            await _db.SaveChangesAsync();

            await using var fs = System.IO.File.OpenWrite(Path.Combine(storeDir, hash));
            fileStream.Position = 0;
            await fileStream.CopyToAsync(fs);
            return Ok(new CertUploadResult()
            {
                Hash = hash,
                Issuer = cert.Issuer,
                NotAfter = cert.NotAfter,
                NotBefore = cert.NotBefore,
            });
        }

        [HttpPut("img")]
        public async Task<IActionResult> UploadImg(IFormFile file)
        {
            var fileStream = file.OpenReadStream();
            var md5Hasher = MD5.Create();
            var hash = md5Hasher.ComputeHash(fileStream).ToHexStr();
            var prefix = hash.Substring(0, 3);
            var storeDir = Path.Combine(_virtualRoot, GlobalConst.RelFilesPath, prefix);
            if (!Directory.Exists(storeDir))
                Directory.CreateDirectory(storeDir);

            var dbFile = new DbFile()
            {
                CreateDate = DateTime.UtcNow,
                Md5Hash = hash,
                Type = EFileType.Image
            };
            _db.Files.Add(dbFile);
            await _db.SaveChangesAsync();

            await using var fs = System.IO.File.OpenWrite(Path.Combine(storeDir, hash));
            fileStream.Position = 0;
            await fileStream.CopyToAsync(fs);
            return Ok(new UploadResult()
            {
                Hash = hash
            });
        }

        [HttpPut("bin")]
        public async Task<IActionResult> UploadBinary(IFormFile file)
        {
            var fileStream = file.OpenReadStream();
            var md5Hasher = MD5.Create();
            var hash = md5Hasher.ComputeHash(fileStream).ToHexStr();
            var prefix = hash.Substring(0, 3);
            var storeDir = Path.Combine(_virtualRoot, GlobalConst.RelFilesPath, prefix);
            if (!Directory.Exists(storeDir))
                Directory.CreateDirectory(storeDir);

            var dbFile = new DbFile()
            {
                CreateDate = DateTime.UtcNow,
                Md5Hash = hash,
                Type = EFileType.Binary
            };
            _db.Files.Add(dbFile);
            await _db.SaveChangesAsync();

            await using var fs = System.IO.File.OpenWrite(Path.Combine(storeDir, hash));
            fileStream.Position = 0;
            await fileStream.CopyToAsync(fs);
            return Ok(new UploadResult()
            {
                Hash = hash
            });
        }
        #endregion

        [HttpDelete("{md5}")]
        public async Task<IActionResult> DeleteAsync(string md5)
        {
            if (md5.Length != 32)
            {
                ModelState.AddModelError("Md5", "Invalid md5 string");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            md5 = md5.ToLower();
            var prefix = md5.Substring(0, 3);
            var filePath = Path.Combine(_virtualRoot, GlobalConst.RelFilesPath, prefix, md5);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(new ProblemDetails()
                {
                    Status = StatusCodes.Status404NotFound,
                    Detail = $"File with hash {md5} not found",
                    Title = "File Not Found"
                });
            }

            var dbFile = await _db.Files.FirstOrDefaultAsync(x => x.Md5Hash == md5);
            _db.Files.Remove(dbFile);
            await _db.SaveChangesAsync();
            System.IO.File.Delete(filePath);
            return Ok();
        }
    }
}