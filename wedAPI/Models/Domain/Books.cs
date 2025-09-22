namespace MyWebApi.Models.Domain
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int PublisherId { get; set; }

        // Quan hệ 1-n (Book - Publisher)
        public Publishers Publisher { get; set; }

        // Quan hệ n-n (Book - Author)
        public ICollection<Book_Author> Book_Authors { get; set; }
    }
}
