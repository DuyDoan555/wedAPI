namespace WebAPI_simple.Models.Domain
{
    public class Book_Author
    {
        public int Id { get; set; }

        // FK đến Book
        public int BookId { get; set; }
        public Book Book { get; set; }

        // FK đến Author
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
