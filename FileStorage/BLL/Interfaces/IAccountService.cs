using System.Threading.Tasks;
using BLL.DTO.Authentication;

namespace BLL.Interfaces
{
    public interface IAccountService
    {
        public Task<object> Login(LoginModel model);

        public Task<string> Register(RegisterModel model, string pathToFolder);
    }
}
