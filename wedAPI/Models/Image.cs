using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_simple.Models.Domain
{
    public class Image
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileDescription { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }  // File upload từ client, không lưu DB
    }
}
