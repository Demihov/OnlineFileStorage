using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IFileRepository
    {
        public Task<FileModel> Get(int id);
        public FileModel Insert(FileModel file);
        public void Delete(int id);
        public Task<IEnumerable<FileModel>> GetFilesByUser(string userId);
    }
}
