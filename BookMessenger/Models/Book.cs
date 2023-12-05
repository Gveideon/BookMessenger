namespace BookMessenger.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Author> Authors { get; set; }   
        public List<AuthorBook> AuthorBooks { get; set; }
        public List<Genre> Genres { get; set; }
        public List<UserBook> UserBooks { get; set; }
        public List<UserProfile> UserProfiles { get; set; }

    }
}
