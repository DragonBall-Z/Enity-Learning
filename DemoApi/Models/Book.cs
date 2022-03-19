namespace DemoApi.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public int? pages { get; set; }
        public int? price  { get; set; }

        public ICollection<Book_Author> Book_Authors { get; set; }
    }
}
