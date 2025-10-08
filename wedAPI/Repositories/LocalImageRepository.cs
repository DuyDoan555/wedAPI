using WebAPI_simple.Data;
using WebAPI_simple.Models.Domain;

namespace WebAPI_simple.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, AppDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public async Task<Image> Upload(Image image)
        {
            var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", image.FileName);
            using (var stream = new FileStream(localPath, FileMode.Create))
            {
                await image.File.CopyToAsync(stream);
            }

            image.FilePath = localPath;
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task<IEnumerable<Image>> GetAll()
        {
            return _context.Images.ToList();
        }

        public async Task<Image> GetById(int id)
        {
            return await _context.Images.FindAsync(id);
        }
    }
}
