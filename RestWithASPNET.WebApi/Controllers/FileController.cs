using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Application.Services.File;
using RestWithASPNET.Domain.Entities;
using RestWithASPNET.Domain.ValueObjects;

namespace RestWithASPNET.WebApi.Controllers
{
    [ApiVersion("1")]
    [Authorize("Bearer")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet("downloadFile/{name}")]
        [ProducesResponseType((200), Type = typeof(byte[]))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[Produces("application/octet-stream")]
        public async Task<IActionResult> GetFileAsync(string name)
        {
            var folderName = Path.Combine("UploadDir");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fileName = name;
            var fullPath = Path.Combine(pathToSave, fileName);

            // fileDetail.DocUrl = Path.Combine(baseUrl + "/api/file/v1/" + fileDetail.DocumentName);

            if (!System.IO.File.Exists(fullPath))
            {
                return null;
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            var fileContentResult = new FileContentResult(fileBytes, "application/octet-stream")
            {
                FileDownloadName = fileName,
            };

            return fileContentResult;
        }


        [HttpPost("uploadFile"), DisableRequestSizeLimit]
        [ProducesResponseType((200), Type = typeof(FileDetailVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UploadOneFile([FromForm] FileUploadModel model)
        {
            if (model.File == null && model.File.Length == 0) return BadRequest("Invalid file");

            FileDetailVO res = await _fileService.SaveFileToDisk(model.File);

            if (res.DocumentName == "") return BadRequest($"Arquivo {res.DocUrl} já existe!");

            return Ok($"Arquivo {res.DocumentName} copiado com sucesso!");
        }

        [HttpPost("uploadMultipleFiles")]
        [ProducesResponseType((200), Type = typeof(List<FileDetailVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UploadManyFiles([FromForm] FileUploadManyModel model)
        {
            var response = new Dictionary<string, string>();

            if (model.Files == null || model.Files.Count == 0) return BadRequest("Invalid file");

            var res = await _fileService.SaveFilesToDisk(model.Files);

            return new OkObjectResult(response);
        }
    }
}
