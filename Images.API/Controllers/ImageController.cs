using Images.API.DTO_s;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Images.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        public static IHostEnvironment _environment;

        public ImageController(IHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        [Route("Player")]
        public async Task<IActionResult> Player(PlayerInsertImageDTO player)
        {
            var folderName = "MyLocalImages/";
            var fileName = await CopyImage(player.Image, folderName);

            return Ok(fileName);
        }


        private async Task<string> CopyImage(byte[] imageArray, string directory)
        {
            if (imageArray != null && imageArray.Length > 0)
            {
                var stream = new MemoryStream(imageArray);
                string fileName = Guid.NewGuid() + ".png";
                IFormFile file = new FormFile(stream, 0, imageArray.Length, "name", fileName);
                string fName = file.FileName;
                string path = Path.Combine(_environment.ContentRootPath, directory + file.FileName);
                using var stream1 = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream1);

                return fileName;
            }
            return null;
        }

    }
}
