using Microsoft.AspNetCore.Http;
using RestWithASPNET.Domain.ValueObjects;

namespace RestWithASPNET.Application.Services.File
{
    public interface IFileService
    {
        public byte[] GetFile(string filename);
        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);
        public Task<Dictionary<string, string>> SaveFilesToDisk(IList<IFormFile> file);
    }
}
