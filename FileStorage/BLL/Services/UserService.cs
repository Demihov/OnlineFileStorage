using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.DTO.Authentication;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using BLL.Exceptions;
using System.IO;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly ICustomFileProvider _customFileProvider;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICustomFileProvider customFileProvider,
            UserManager<User> userManager)
        {
            _mapper = mapper;
            _customFileProvider = customFileProvider;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDTO> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new ObjectNotFoundException($"user with id {id} not found");
            }

            return _mapper.Map<User, UserDTO>(user);
        }

        public async Task<UserDTO> UpdateUser(UserUpdateModel userToUpdate)
        {
            var user = await _userManager.FindByIdAsync(userToUpdate.Id);
            await _userManager.UpdateAsync(user);
            return _mapper.Map<User, UserDTO>(user);
        }

        public async Task DeleteUser(string contentRootPath, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new ObjectNotFoundException($"user with id {id} not found");

            var path = Path.Combine(contentRootPath, id);
            _customFileProvider.DeleteFolder(path);

            await _userManager.DeleteAsync(user);
        }

        public async Task UserToAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.AddToRoleAsync(user, "Administrator");
        }

        public async Task AdminToUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.RemoveFromRoleAsync(user, "Administrator");
        }
    }
}
