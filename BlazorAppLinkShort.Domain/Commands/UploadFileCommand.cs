using Flunt.Notifications;
using Flunt.Validations;
using Microsoft.AspNetCore.Http;
using TestUpload.Domain.Commands;

namespace TestUploadFile.Domain.Commands
{
    public class UploadFileCommand : Notifiable<Notification>, ICommand
    {
        public ICollection<IFormFile>? Files { get; set; }
        public List<object>? UploadedFilesData { get; set; } 

        public UploadFileCommand(ICollection<IFormFile>? files)
        {
            Files = files;
            UploadedFilesData = new List<object>();
        }

        // Validate
        public bool Validate()
        {
            AddNotifications(new Contract<UploadFileCommand>()
                .Requires()
                .IsNotNull(Files, "Files", "No file was uploaded.")
                .IsGreaterThan(Files?.Count ?? 0, 0, "Files", "You must upload at least one file.")
            );

            return IsValid;
        }

       
        public void AddUploadedFileData(string fileName, string filePath, string fileCode)
        {
            UploadedFilesData?.Add(new
            {
                FileName = fileName,
                FilePath = filePath,
                FileCode = fileCode
            });
        }

        //Method to get data from uploaded files
        public IEnumerable<object>? GetUploadedFilesData()
        {
            return UploadedFilesData;
        }

        
        public IReadOnlyCollection<Notification> GetNotifications()
        {
            return Notifications;
        }
    }
}
