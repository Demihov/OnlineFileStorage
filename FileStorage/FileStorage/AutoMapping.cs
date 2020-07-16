using AutoMapper;
using BLL.DTO;
using BLL.DTO.Authentication;
using BLL.DTO.File;
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
            CreateMap<UserUpdateModel, User>();

            CreateMap<FilePostRequest, FileModel>();
        }
    }
}
