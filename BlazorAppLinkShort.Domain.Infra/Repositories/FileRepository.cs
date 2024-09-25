using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IRepositories;
using TestUpload.Domain.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infra.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly DatabaseContext _context;

        public FileRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(FileEntity fileEntity)
        {
            _context.FilesApplication.Add(fileEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<FileEntity> GetByCodeAsync(string fileCode)
        {
            return await _context.FilesApplication.SingleOrDefaultAsync(f => f.FileCode == fileCode);
        }

        public async Task<FileEntity?> GetByFileCodeAsync(string fileCode)
        {
            return await _context.FilesApplication.FirstOrDefaultAsync(f => f.FileCode == fileCode);
        }
    }

}
