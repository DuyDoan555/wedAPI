namespace MyWebApi.Models.Domain
{
    public class Publishers
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // 1 Publisher - nhiều Books
        public ICollection<Books> Books { get; set; }
    }
}
