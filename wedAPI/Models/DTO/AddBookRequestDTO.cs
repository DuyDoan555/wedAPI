using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebAPI_simple.Models.DTO
{
    public class AddBookRequestDTO
    {
        [Required(ErrorMessage = "Title is required")]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Title cannot contain special characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }

        [Range(0, 5, ErrorMessage = "Rate must be between 0 and 5")]
        public int? Rate { get; set; }

        public string? Genre { get; set; }
        public string? CoverUrl { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "PublisherID is required")]
        public int PublisherID { get; set; }

        [Required(ErrorMessage = "Book must have at least 1 author")]
        public List<int> AuthorIds { get; set; } = new();
    }
}
