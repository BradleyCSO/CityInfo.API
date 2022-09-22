using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            // Can use FileContentResult, FileStreamResult, PhysicalFileResult, VirtualFileResult : all which derive from the same class
            // For now, we will return File() which is defined as part of the controller, and acts as a wrapper around the forementioned classes
            string filePath = "blank.pdf";

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
            
            var bytes = System.IO.File.ReadAllBytes(filePath);

            return File(bytes, "application/pdf", Path.GetFileName(filePath));
        }
    }
}
