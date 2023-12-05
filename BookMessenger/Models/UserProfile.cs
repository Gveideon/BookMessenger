namespace BookMessenger.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public List<Message> Messages { get; set; } = new();
        public List<Book> Books { get; set; } = new();
        public List<UserBook> UserBooks { get; set; } = new();
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
