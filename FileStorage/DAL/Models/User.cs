using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace DAL.Models
{
    public class User: IdentityUser
    {
        public int Year { get; set; }

        public ICollection<FileModel> FileModels { get; set; }
    }
}
