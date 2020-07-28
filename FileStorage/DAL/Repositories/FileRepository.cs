using DAL.DbContexts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using DAL.Interfaces;
using System.Threading.Tasks;
using DAL.Models.Pagination;

namespace DAL.Repositories
{
    public class FileRepository : IFileRepository
    {
        private ApplicationDbContext _context;

        public FileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FileModel> Get(int id)
        {
            return await _context.Files.FindAsync(id);
        }

        public FileModel Insert(FileModel file)
        {
            return _context.Files.Add(file).Entity;
        }

        public void Delete(int id)
        {
            FileModel file = _context.Files.Find(id);
            if (file != null)
                _context.Remove(file);
        }

        public async Task<IEnumerable<FileModel>> GetFilesByUser(string userId)
        {
            return await _context.Files.Where(i => i.User.Id == userId).ToListAsync();
        }

        public async Task<PagedList<FileModel>> GetFilesByUser(string userId, PaginationParameters parameters)
        {
            var files = _context.Files
                .Where(i => i.UserId == userId)
                .OrderByDescending(on => on.Name);

            return PagedList<FileModel>.ToPagedList(files, parameters.PageNumber, parameters.PageSize);
        }
    }
}
