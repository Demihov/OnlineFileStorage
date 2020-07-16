using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.AspNetCore.Http;

namespace BLL.Interfaces
{
    public interface ICustomFileProvider
    {
        public Task AddFile(string path, IFormFile file);
        public Stream GetFile(string path);
        public void DeleteFile(string path);
    }
}
