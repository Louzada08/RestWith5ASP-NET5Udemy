using Microsoft.AspNetCore.Http;
using RestWithASPNET.Domain.Interfaces;
using RestWithASPNET.Domain.ValueObjects;

namespace RestWithASPNET.FrameWrkDrivers.Services.File
{
    public class FileServiceImplementation : IFileService
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _context;

        public FileServiceImplementation(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
        }

        public byte[] GetFile(string filename)
        {
            //var filePath = _basePath + filename;
            //return File.ReadAllBytes(filePath);
            return new byte[1];
        }

        public async Task<FileDetailVO> SaveFileToDisk(IFormFile file)
        {
            FileDetailVO fileDetail = new FileDetailVO();

            var fileType = Path.GetExtension(file.FileName);
            var baseUrl = _context.HttpContext.Request.Host;

            if (fileType.ToLower() != ".exe" && fileType.ToLower() != ".bat" &&
                fileType.ToLower() != ".com" && fileType.ToLower() != ".dll")
            {
                var docName = Path.GetFileName(file.FileName);
                if (file != null && file.Length > 0)
                {
                    var destination = Path.Combine(_basePath, "", docName);
                    fileDetail.DocumentName = docName;
                    fileDetail.DocType = fileType;
                    fileDetail.DocUrl = Path.Combine(baseUrl + "/api/file/v1/" + fileDetail.DocumentName);

                    if (System.IO.File.Exists(destination))
                    {
                        fileDetail.DocumentName = "";
                        return fileDetail;
                    }
                    using var stream = new FileStream(destination, FileMode.Create);
                    await file.CopyToAsync(stream);

                }
            }
            return fileDetail;
        }

        public async Task<Dictionary<string, string>> SaveFilesToDisk(IList<IFormFile> files)
        {
            var response = new Dictionary<string, string>();

            foreach (var file in files)
            {
                var folderName = Path.Combine("UploadDir", "AllFiles");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }

                var fileName = file.FileName;
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);

                if (!System.IO.File.Exists(fullPath))
                {
                    using var memoryStream = new MemoryStream();
                    await file.CopyToAsync(memoryStream);
                    await System.IO.File.WriteAllBytesAsync(fullPath, memoryStream.ToArray());
                }
                else
                {
                    response.Add(fileName, "Already exists");
                }
            }

            return response;
        }

    }
}
