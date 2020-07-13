using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.DTO.Authentication;
using DAL.Models;

namespace FileStorage
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<FileModel, FileModelDTO>();
            CreateMap<FileModelDTO, FileModel>();

            CreateMap<RegisterModel, User>();
        }
    }
}
