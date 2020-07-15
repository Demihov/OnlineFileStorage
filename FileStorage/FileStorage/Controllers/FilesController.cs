using System.IO;
using System.Threading.Tasks;
using BLL.DTO.File;
using BLL.Interfaces;
using Microsoft.AspNetCore.Hosting;
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

        private string GetContentRootPath()
        {
            return Path.Combine(_appEnvironment.ContentRootPath, "Files");
        }

        // GET: api/Files
        [HttpGet]
        public object Get()
        {
            var result = new { Test = "web api works!" };
            return result;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetFileById(int id)
        {
            var file = await _fileService.GetFile(GetContentRootPath(), id);

            if (file == null)
                return NotFound("File not found");

            return Ok(file);
        }

        [HttpPost]
        public async Task<IActionResult> AddFile([FromForm]FilePostRequest request)
        {
            if (request != null)
            {
                var file = await _fileService.Insert(request, GetContentRootPath());
                return CreatedAtAction(nameof(GetFileById), new {id = file.Id}, file);
            }
            else
            {
                return BadRequest();
            }
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
