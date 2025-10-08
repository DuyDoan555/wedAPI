using Microsoft.AspNetCore.Mvc;
using WebAPI_simple.Models.Domain;
using WebAPI_simple.Repositories;

namespace WebAPI_simple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] Image image)
        {
            ValidateFileUpload(image);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _imageRepository.Upload(image);
            return Ok(image);
        }

        private void ValidateFileUpload(Image image)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(image.File.FileName);

            if (!allowedExtensions.Contains(extension))
                ModelState.AddModelError("file", "Unsupported file format");

            if (image.File.Length > 10485760)
                ModelState.AddModelError("file", "File size cannot exceed 10MB");
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var images = await _imageRepository.GetAll();
            return Ok(images);
        }

        [HttpGet("Download/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var image = await _imageRepository.GetById(id);
            if (image == null)
                return NotFound();

            var fileBytes = await System.IO.File.ReadAllBytesAsync(image.FilePath);
            return File(fileBytes, "application/octet-stream", image.FileName);
        }
    }
}
