using DAL.DbContexts;
using System;
using System.Collections.Generic;
using System.IO;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using DAL.Interfaces;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class FileRepository : IFileRepository
    {
        private ApplicationDbContext db;

        public FileRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public IEnumerable<FileModel> GetAll()
        {
            return db.Files;
            //return db.Categories.Include(c => c.Products);
        }
        public async Task<FileModel> Get(int id)
        {
            var file = await db.Files.FindAsync(id);
            //var category = db.Categories.Single(c => c.Id == id);

            //db.Entry(category).Collection(c => c.Products).Load();

            return file;
        }

        public FileModel Insert(FileModel file)
        {
            if (file != null)
                db.Files.Add(file);
            return file;
        }
        public void Update(FileModel file)
        {
            db.Entry(file).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            FileModel file = db.Files.Find(id);
            if (file != null)
                db.Remove(file);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
