namespace WebAPI_simple.Models.DTO
{
    public class BookWithAuthorAndPublisherDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string? Genre { get; set; }
        public string? CoverUrl { get; set; }
        public string PublisherName { get; set; } = string.Empty;
        public List<string> AuthorNames { get; set; } = new();
    }
}
