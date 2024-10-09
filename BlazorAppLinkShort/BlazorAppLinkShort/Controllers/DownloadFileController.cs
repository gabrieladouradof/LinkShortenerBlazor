
using Domain.Entities;
using Domain.Handlers;
using Domain.Infra.Repositories;
using Domain.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using System.IO;
using System.Threading.Tasks;

namespace TestUploadFile.Api.Controllers
{
    [ApiController]
    public class DownloadFileController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;

        public DownloadFileController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

    [HttpGet]
    [Route("v1/download")]
        public async Task<IActionResult> Download(string code)
    {
        //Search the file in the database by code
        var fileEntity = await _fileRepository.GetByFileCodeAsync(code);

        if (fileEntity == null)
        {
            return NotFound("Arquivo não encontrado.");
        }

         // Checks if file exists on disk
         if (!System.IO.File.Exists(fileEntity.FilePath))
          {
            return NotFound("Arquivo não encontrado no servidor.");
          }

        //read the file and return it as a fileresult
        var fileBytes = await System.IO.File.ReadAllBytesAsync(fileEntity.FilePath);
        return File(fileBytes, "application/octet-stream", Path.GetFileName(fileEntity.FilePath));
    }
}
}
