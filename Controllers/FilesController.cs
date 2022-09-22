using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
        }

        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            // Can use FileContentResult, FileStreamResult, PhysicalFileResult, VirtualFileResult : all which derive from the same class
            // For now, we will return File() which is defined as part of the controller, and acts as a wrapper around the forementioned classes
            const string filePath = "blank.pdf";

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            if (!_fileExtensionContentTypeProvider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream"; // If a type cannot be determined
            }
            
            var bytes = System.IO.File.ReadAllBytes(filePath);

            return File(bytes, contentType, Path.GetFileName(filePath));
        }
    }
}
