namespace BookMessenger.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<Author> Authors { get; set; } = new();
        public List<AuthorBook> AuthorBooks { get; set; } = new();
        public List<Genre> Genres { get; set; } = new();
        public List<UserBook> UserBooks { get; set; } = new();
        public List<UserProfile> UserProfiles { get; set; } = new();

    }
}
