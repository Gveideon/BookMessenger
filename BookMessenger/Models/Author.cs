namespace BookMessenger.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Book> Books { get; set; }   
        public List<AuthorBook> AuthorBooks { get; set; }
    }
}
