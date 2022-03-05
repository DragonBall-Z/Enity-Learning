namespace DemoApi.Models
{
    public class Author
    {
        public Guid Id { get; set; }

        public string AuthorName { get; set; }
        public string PoeticName { get; set; }

        public ICollection<Book_Author> Book_Authors{get;set;}

    }
}
