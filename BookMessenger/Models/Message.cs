namespace BookMessenger.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime? Date { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile? User { get; set; }
        public int DiscussId { get; set; }
        public Discuss? Discuss { get; set; }
    }
}
