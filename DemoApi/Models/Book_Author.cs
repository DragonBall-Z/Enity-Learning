namespace DemoApi.Models
{
    public class Book_Author
    {
       
        public Guid Id { get; set; }//pk

        public Guid BookId { get; set; }

        public Book Book { get; set; }


        public Guid AuthorId { get; set; }

        public Author Author{ get; set; }



    }
}
