using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IFileRepository
    {
        Task SaveAsync(FileEntity fileEntity);
        Task<FileEntity> GetByCodeAsync(string fileCode);
        Task<FileEntity?> GetByFileCodeAsync(string fileCode);
    }
}
