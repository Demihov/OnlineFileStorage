using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.DTO.File;
using BLL.DTO.Pagination;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using DAL.Models.Pagination;

namespace BLL.Services
{
    public class FilesService : IFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICustomFileProvider _fileProvider;

        public FilesService(IUnitOfWork unitOfWork, IMapper mapper, ICustomFileProvider customFileProvider)
        {
            _fileProvider = customFileProvider;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<Stream> GetFile(string contentRootPath, string userId, int fileId)
        {
            var file = await _unitOfWork.FileRepository.Get(fileId);
            if (file == null)
                throw new FileNotFoundException($"file with id {fileId} not found");

            return _fileProvider.GetFile(Path.Combine(contentRootPath, userId, file.Name));
        }

        public Task<FileModelDTO> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedListDTO<FileModelDTO>> GetFiles(string userId, PaginationParametersDTO pagination)
        {
            var options = _mapper.Map<PaginationParametersDTO, PaginationParameters>(pagination);

            var pagedList = await _unitOfWork.FileRepository.GetFilesByUser(userId, options);

            var result = _mapper.Map<PagedList<FileModel>, PagedListDTO<FileModelDTO>>(pagedList);

            return result;
        }

        public async Task<FileModelDTO> Insert([Required] FilePostRequest request, string contentRootPath)
        {
            var file = _mapper.Map<FilePostRequest, FileModel>(request);

            file.Name = request.File.FileName;

            string path = Path.Combine(contentRootPath, request.UserId);

            await _fileProvider.AddFile(path, request.File);

            _unitOfWork.FileRepository.Insert(file);
            await _unitOfWork.Save();

            return _mapper.Map<FileModel, FileModelDTO>(file);
        }

        public async Task DeleteFile(string contentRootPath, string userId, int fileId)
        {
            var file = await _unitOfWork.FileRepository.Get(fileId);

            //if (file == null)
            //throw new ObjectNotFoundException();
            //if (file.UserId != userId)
            //    throw new FileAccessException();

            var filePath = Path.Combine(contentRootPath, userId, fileId.ToString());

            _fileProvider.DeleteFile(filePath);
            _unitOfWork.FileRepository.Delete(fileId);

            await _unitOfWork.Save();
        }
    }
}
