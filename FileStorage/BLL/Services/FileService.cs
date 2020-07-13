using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
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

            return GetByPath(Path.Combine(contentRootPath, file.Name));
        }

        public Task<FileModelDTO> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileModelDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Stream GetByPath(string path)
        {
            return File.Open(path, FileMode.Open);
        }

        public FileModelDTO Insert(FileModelDTO item)
        {
            FileModel fileModel = _unitOfWork.FileRepository.Insert(_mapper.Map<FileModelDTO, FileModel>(item));
            _unitOfWork.Save();

            return _mapper.Map<FileModel, FileModelDTO>(fileModel);
        }

        public void Update(FileModelDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
