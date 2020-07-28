using AutoMapper;
using BLL.DTO;
using BLL.DTO.Authentication;
using BLL.DTO.File;
using BLL.DTO.Pagination;
using DAL.Models;
using DAL.Models.Pagination;

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

            CreateMap<PaginationParametersDTO, PaginationParameters>();
            CreateMap<PagedList<FileModel>, PagedListDTO<FileModelDTO>>();
        }
    }
}
