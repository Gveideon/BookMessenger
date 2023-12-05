namespace BookMessenger.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public List<Message> Messages { get; set; }
        public List<Book> Books { get; set; }
        public List<UserBook> UserBooks { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
