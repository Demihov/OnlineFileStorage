using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DAL.DbContexts;
using DAL.Models;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext db;
        private FileRepository fileRepository;

        public EFUnitOfWork(ApplicationDbContext context)
        {
            db = context;
        }

        public IFileRepository FileRepository => fileRepository ?? new FileRepository(db);


        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
