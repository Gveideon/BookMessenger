namespace BookMessenger.Models
{
    
    public class Role
    {
        public int Id { get; set; }
        public TypeRole TypeRole { get; set; } = TypeRole.None;
    }
}
