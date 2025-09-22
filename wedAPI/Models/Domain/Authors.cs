namespace WebAPI_simple.Models.Domain
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        // Quan hệ n-n với Book
        public List<Book_Author> Book_Authors { get; set; }
    }
}
