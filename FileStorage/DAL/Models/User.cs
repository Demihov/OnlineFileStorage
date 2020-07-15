using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DAL.Models
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName{ get; set; }

        public ICollection<FileModel> FileModels { get; set; }
    }
}
