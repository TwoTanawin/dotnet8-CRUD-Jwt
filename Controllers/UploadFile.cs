using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ControllerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadFile : ControllerBase
    {
        private readonly string _uploadsDir;

        public UploadFile(){
            _uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(_uploadsDir)){
                Directory.CreateDirectory(_uploadsDir);
            }
        }

        // [HttpPost]
        // public async Task<IActionResult> FileUpload
    }
}