using RestWithASPNETUdemy.Data.VO;

namespace RestWithASPNETUdemy.Services
{
    public interface IFileService
    {
        public byte[] GetFile(string filename);
        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);
        public Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> file);
    }
}
