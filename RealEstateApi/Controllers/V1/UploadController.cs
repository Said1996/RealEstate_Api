using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RealEstateApi.Models.Request;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RealEstateApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {

        private readonly IWebHostEnvironment env;

        public FileUploadController(IWebHostEnvironment env)
        {
            this.env = env;
        }

        [HttpPost]
        public async Task<ActionResult<string>> UploadProfilePicture(UploadedFile uploadedFile)
        {
            var path = Path.Combine("Resources", "Images", Guid.NewGuid().ToString() + "_" + uploadedFile.FileName);
            var pathToSave = Path.Combine(env.WebRootPath, path);
            var dbPath = Path.Combine("https://localhost:44388/", path);
            var fs = System.IO.File.Create(pathToSave);
            await fs.WriteAsync(uploadedFile.FileContent, 0, uploadedFile.FileContent.Length);
            fs.Close();
            return Ok(dbPath);
        }
    }
}
