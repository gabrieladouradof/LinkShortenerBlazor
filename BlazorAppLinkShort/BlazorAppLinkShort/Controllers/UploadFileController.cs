using Domain.Handlers;
using Domain.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TestUpload.Domain.Commands;
using TestUploadFile.Domain.Commands;

namespace TestUploadFile.Controllers
{
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;
        private readonly UploadFileHandler _uploadFileHandler;
        private readonly string _uploadFolder;

        public UploadFileController(UploadFileHandler uploadFileHandler, IFileRepository fileRepository)
        {
            _uploadFileHandler = uploadFileHandler;
            _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            _fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("/fileupload2")]
        public async Task<IActionResult> Upload([FromForm] ICollection<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado.");
            }

            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }

            var command = new UploadFileCommand(files);

            // command validation
            if (!command.Validate())
            {
                return BadRequest(command.GetNotifications());
            }

            ICommandResult result = await _uploadFileHandler.Handle(command);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            // Return the result the handle
            return Ok(new
            {
                success = result.Success,
                message = result.Message,
                files = result.Data
            }); ;
            
        }
    }
}
