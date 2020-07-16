using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.DTO.Authentication;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUser(string id);
        Task<UserDTO> UpdateUser(UserUpdateModel request);
        Task DeleteUser(string id);
        Task UserToAdmin(string id);
        Task AdminToUser(string id);
        //Task<PaginationResponse<UserWithRolesDto>> SearchUsersByName(string username, PaginationRequest request);
    }
}
