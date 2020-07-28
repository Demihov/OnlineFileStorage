using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _appEnvironment;

        public UsersController(
            IUserService userService,
            IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            _userService = userService;
        }

        private string GetCurrentPath()
        {
            return Path.Combine(_appEnvironment.ContentRootPath, "Files");
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userService.GetUser(id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUser(GetCurrentPath(), id);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/toAdmin")]
        public async Task<IActionResult> UserToModerator(string id)
        {
            await _userService.UserToAdmin(id);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/toNormalUser")]
        public async Task<IActionResult> ModeratorToUser(string id)
        {
            await _userService.AdminToUser(id);
            return NoContent();
        }
    }
}
