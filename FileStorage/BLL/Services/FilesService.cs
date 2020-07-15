using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.DTO.File;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

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
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Stream> GetFile(string contentRootPath, int id)
        {
            var file = await _unitOfWork.FileRepository.Get(id);
            if (file == null)
                throw new FileNotFoundException($"file with id {id} not found");

            return _fileProvider.GetFile(Path.Combine(contentRootPath, file.Name));
        }

        public Task<FileModelDTO> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileModelDTO> GetAll()
        {
            throw new NotImplementedException();
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

        public void Update(FileModelDTO item)
        {
            throw new NotImplementedException();
        }

        public Stream GetByPath(string path)
        {
            throw new NotImplementedException();
        }


    }
}
