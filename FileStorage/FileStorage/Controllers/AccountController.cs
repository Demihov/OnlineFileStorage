using System.IO;
using System.Threading.Tasks;
using BLL.DTO.Authentication;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace FileStorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IAccountService _accountService;
        private readonly IWebHostEnvironment _appEnvironment;

        public AccountController(
            IWebHostEnvironment appEnvironment,
            UserManager<User> userManager,
            IAccountService accountService
            )
        {
            _appEnvironment = appEnvironment;
            _userManager = userManager;
            _accountService = accountService;
        }

        private async Task<User> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(User);
        }

        private string GetCurrentPath()
        {
            return Path.Combine(_appEnvironment.ContentRootPath, "Files");
        }


        //[Authorize(Roles = "NormalUser")]
        [Authorize]
        [HttpGet("protected")]
        public async Task<object> Protected()
        {
            //return User.IsInRole("NormalUser");

            //var userId = await GetCurrentUserAsync().Id;

            var id = _userManager.GetUserId(User);
            return id;
        }

        [HttpPost("login")]
        public async Task<object> Login([FromBody] LoginModel model)
        {
            
            return await _accountService.Login(model);
        }

        [HttpPost("register")]
        public async Task<object> Register([FromBody] RegisterModel model)
        {
            return await _accountService.Register(model, GetCurrentPath());
        }
    }
}