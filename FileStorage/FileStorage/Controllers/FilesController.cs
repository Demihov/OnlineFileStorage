using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
       private readonly IWebHostEnvironment _appEnvironment;
       private readonly IFileService _fileService;
        public FilesController(IWebHostEnvironment appEnvironment, IFileService fileService)
        {
            _appEnvironment = appEnvironment;
            _fileService = fileService;
        }

        //// GET: api/Files
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    List<string> paths = new List<string>();
        //    paths.Add(_appEnvironment.WebRootPath);
        //    paths.Add(_appEnvironment.ContentRootPath);

        //    return paths;
        //}

        // GET: api/Files
        [HttpGet]
        public object Get()
        {
            var result = new { Test = "web api works!" };
            return result;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetFileByPath(int id)
        {
            var file = await _fileService.GetFile(_appEnvironment.ContentRootPath + "/Oleh/", id);

            if (file == null)
                return NotFound("File not found");

            return Ok(file);
        }

        // POST: api/Files
        [HttpPost]
        public async Task<IActionResult> AddFile([FromForm]IFormFile uploadedFile)
        {
            //var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.ContentRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                FileModelDTO file = new FileModelDTO { Name = uploadedFile.FileName, Path = path };

                _fileService.Insert(file);
            }
            else
            {
                return BadRequest();
            }

            //return RedirectToAction("Index");
            return Ok();
        }

        // PUT: api/Files/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
