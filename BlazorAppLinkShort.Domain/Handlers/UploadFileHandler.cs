using Domain.Entities;
using Domain.IRepositories;
using Microsoft.AspNetCore.Http;
using System.Net;
using TestUpload.Domain.Commands;
using TestUploadFile.Domain.Commands;

namespace Domain.Handlers
{
    public class UploadFileHandler
    {
        private readonly IFileRepository _fileRepository;
        private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        public UploadFileHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<ICommandResult> Handle(UploadFileCommand command)
        {
            if (command.Files == null || !command.Files.Any())
            {
                return new CommandResult(HttpStatusCode.BadRequest, false, "Nenhum arquivo foi enviado.");
            }

            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }

            // Creating a list to store file data
            var uploadedFilesData = new List<object>(); 

            foreach (var formFile in command.Files)
            {
                var fileCode = GenerateFileCode(6);
                //to print and keep the same name
                var originalFileName = Path.GetFileName(formFile.FileName);
                var filePath = Path.Combine(_uploadFolder, $"{originalFileName}");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                // Save the informations in the database
                var fileEntity = new FileEntity(filePath, fileCode);
                
                await _fileRepository.SaveAsync(fileEntity);

                // Add the code and file path to the list
                uploadedFilesData.Add(new
                {
                    fileCode,
                    filePath
                });
            }

            //Returning file data in command result
            return new CommandResult(HttpStatusCode.OK, true, "Arquivos enviados com sucesso!", uploadedFilesData);
        }

        private async Task<string> SaveFileToDisk(IFormFile formFile, string fileCode)
        {
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var filePath = Path.Combine(uploadFolder, $"{fileCode}{Path.GetExtension(formFile.FileName)}");

            Console.WriteLine($"Recebendo arquivo {formFile.FileName}");

            // Save the file in system
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await formFile.CopyToAsync(stream);
            }
            return filePath;
        }

        private string GenerateFileCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Range(0, length)
                .Select(_ => chars[random.Next(chars.Length)]).ToArray());
        }
    }
}
