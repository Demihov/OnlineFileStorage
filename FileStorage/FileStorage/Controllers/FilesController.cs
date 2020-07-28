using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using BLL.DTO.File;
using BLL.DTO.Pagination;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFileById(string userId, int id)
        {
            var file = await _fileService.GetFile(GetContentRootPath(),userId, id);

            if (file == null)
                return NotFound("File not found");

            return Ok(file);
        }

        [HttpGet]
        public async Task<IActionResult> GetFiles([Required] string userId, [Required] int page, [Required] int itemCount)
        {
            var request = new PaginationParametersDTO { PageSize = itemCount, PageNumber = page };
            var files = await _fileService.GetFiles(userId, request);

            return Ok(files);
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
    }
}
