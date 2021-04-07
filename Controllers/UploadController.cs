using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace UploadFileUsingDotNETCore.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        string fileUrl;

        [HttpPost("upload", Name = "upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile( IFormFile file, CancellationToken cancellationToken)
        {
            if (CheckIfExcelFile(file))
            {
                await WriteFile(file);
            }
            else
            {
                return BadRequest(new { message = "Invalid file extension" });
            }

            return Ok(fileUrl);
        }

        private bool CheckIfExcelFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".png" || extension == ".jpg" || extension == ".pdf"); 
        }

        private async Task<bool> WriteFile(IFormFile file)
        {
            bool isSaveSuccess = false;
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks + extension; //Create a new Name for the file due to security reasons.

                string path = "";
                if (extension == ".png" || extension == ".jpg")
                {
                     path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Resources\\Images", fileName);
                     fileUrl = "wwwroot/Resources/Images/" + fileName;
                }

                if (extension == ".pdf")
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Resources\\pdf", fileName);
                    fileUrl = "wwwroot/Resources/pdf/" + fileName;
                }

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                isSaveSuccess = true;
            }
            catch (Exception e)
            {
                var ee = e;
                //log error
            }

            return isSaveSuccess;
        }
    }
}
