using Images.WEB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Images.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ConvertImage()
        {
            var fileImage = Request.Form.Files[0];
            string convertImage = ConvertToBase64String(fileImage);//Convert to base64
            //Create object
            var objPlayer = new
            {
                FullName = "Cristiano Ronaldo",
                Image = convertImage
            };
            //Serialize object
            var json = JsonConvert.SerializeObject(objPlayer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            //Call API -  Post Endpoint
            using var httpClient = new HttpClient();
            using var response = await httpClient
                .PostAsync("http://localhost:30651/api/Image/Player", data);
            string apiResponse = await response.Content.ReadAsStringAsync();
            //Return filename
            var fileNameResponse = JsonConvert.DeserializeObject<string>(apiResponse);
            return Json(fileNameResponse);
        }

        private string ConvertToBase64String(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                return Convert.ToBase64String(fileBytes);
            }
        }

        private byte[] ConvertToBytes(IFormFile image)
        {
            byte[] CoverImageBytes = null;
            BinaryReader reader = new BinaryReader(image.OpenReadStream());
            CoverImageBytes = reader.ReadBytes((int)image.Length);
            return CoverImageBytes;
        }     
    }
}
