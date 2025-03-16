using Microsoft.AspNetCore.Http;

namespace RestWithASPNET.Domain.Entities
{
    public class FileUploadManyModel
    {
        public IList<IFormFile> Files { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
    }
}
