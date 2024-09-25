namespace Domain.Entities
{
    public class FileEntity

    {
        //was removed the "privateset" the entities for manipulating the entities in the endpoint
        public int Id { get; private set; }
        public string FilePath { get; set; } = string.Empty;
        public string FileCode { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }  

        public FileEntity(string filePath, string fileCode)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            FileCode = fileCode ?? throw new ArgumentNullException(nameof(fileCode));
            CreatedAt = DateTime.UtcNow;
        }
    }
}

