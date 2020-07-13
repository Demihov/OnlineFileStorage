using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using BLL.DTO.Authentication;

namespace BLL.Services
{
    public class AccountService: IAccountService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IFolderService _folderService;
        private readonly IMapper _mapper;

        public AccountService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IFolderService folderService,
            IMapper mapper
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _folderService = folderService;
            _mapper = mapper;
        }

        public async Task<object> Login(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                return GenerateJwtToken(model.Email, appUser);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        public async Task<object> Register( RegisterModel model, string pathToFolder)
        {
            var user = _mapper.Map<RegisterModel, User>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            await _userManager.AddToRoleAsync(user, "NormalUser");

            if (result.Succeeded)
            {
                _folderService.CreateFolder(Path.Combine(pathToFolder, model.UserName));

                await _signInManager.SignInAsync(user, false);
                return  GenerateJwtToken(model.Email, user);
            }
            else
            {
                return result.Errors;
            }
        }

        private string GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var now = DateTime.UtcNow;

            var signingCredentials = new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
            var expires = now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME));

            var token = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
