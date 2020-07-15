using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IFileRepository FileRepository { get; }
        Task Save();
    }
}
