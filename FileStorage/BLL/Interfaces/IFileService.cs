using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IFileService
    {
        public Task<Stream> GetFile(string contentRootPath, int id);
        public Stream GetByPath(string path);
    }
}
