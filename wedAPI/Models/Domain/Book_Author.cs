namespace MyWebApi.Models.Domain
{
    public class Book_Author
    {
        public int BookId { get; set; }
        public Books Book { get; set; }

        public int AuthorId { get; set; }
        public Authors Author { get; set; }
    }
}
