namespace BookMessenger.Models
{
    public class UserBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile? User { get; set; }
        public int? MarkValue { get; set; } = -1;
        public bool? HasInLibrary { get; set; } = false;
    }
}
