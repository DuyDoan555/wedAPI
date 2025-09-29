using System.ComponentModel.DataAnnotations;

namespace WebAPI_simple.Models.DTO
{
    public class AddPublisherRequestDTO
    {
        [Required(ErrorMessage = "Publisher name is required")]
        public string Name { get; set; }
    }
}
