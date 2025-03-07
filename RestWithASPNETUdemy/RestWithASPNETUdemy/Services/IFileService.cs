using RestWithASPNETUdemy.Data.VO;

namespace RestWithASPNETUdemy.Services
{
    public interface IFileService
    {
        public byte[] GetFile(string filename);
        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);
        public Task<Dictionary<string, string>> SaveFilesToDisk(IList<IFormFile> file);
    }
}
