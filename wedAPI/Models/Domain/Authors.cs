namespace MyWebApi.Models.Domain
{
    public class Authors
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        // Quan hệ n-n với Books
        public ICollection<Book_Author> Book_Authors { get; set; }
    }
}
