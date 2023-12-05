namespace BookMessenger.Models
{
    public class Discuss
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Message> Messages { get; set; } 
    }
}
