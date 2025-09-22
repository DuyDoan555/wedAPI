namespace WebAPI_simple.Models.Domain
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Quan hệ 1-n với Book
        public List<Book> Books { get; set; }
    }
}
