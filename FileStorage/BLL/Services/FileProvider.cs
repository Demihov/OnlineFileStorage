using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace BLL.Services
{
    public class FileProvider: ICustomFileProvider
    {
        public async Task AddFile(string path, IFormFile file)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        public Stream GetFile(string path)
        {
            return File.Open(path, FileMode.Open);
        }

        public void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
