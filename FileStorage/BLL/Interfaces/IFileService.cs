using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.DTO.File;
using BLL.DTO.Pagination;

namespace BLL.Interfaces
{
    public interface IFileService
    {
        public Task<Stream> GetFile(string contentRootPath, string userId, int id);

        public Task<FileModelDTO> Insert(FilePostRequest request, string contentRootPath);

        public Task<PagedListDTO<FileModelDTO>> GetFiles(string userId, PaginationParametersDTO pagination);
    }
}
