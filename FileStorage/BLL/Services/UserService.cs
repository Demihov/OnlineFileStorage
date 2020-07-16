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

namespace BLL.Services
{
    public class UserService: IUserService
    {
        private readonly ICustomFileProvider _customFileProvider;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(
            IMapper mapper,
            ICustomFileProvider customFileProvider,
            UserManager<User> userManager)
        {
            _mapper = mapper;
            _customFileProvider = customFileProvider;
            _userManager = userManager;
        }

        public async Task<UserDTO> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new ObjectNotFoundException($"user with id {id} not found"); ;
            }

            return _mapper.Map<User, UserDTO>(user);
        }

        public async Task<UserDTO> UpdateUser(UserUpdateModel userToUpdate)
        {
            if (userToUpdate == null)
            {
                //throw new UserException()
            }

            var user = await _userManager.FindByIdAsync(userToUpdate.Id);
            if (user == null)
            {
                //throw new 
            }

            await _userManager.UpdateAsync(user);
            return _mapper.Map<User, UserDTO>(user);
        }

        public Task DeleteUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task UserToAdmin(string id)
        {
            throw new NotImplementedException();
        }

        public Task AdminToUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}
