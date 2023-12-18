using System.ComponentModel.DataAnnotations;

namespace BookMessenger.Models
{
    public class AdminActionLog
    {
        public int Id { get; set; }
        public string NameUser { get; set; }
        public TypeRole RolePrevious { get; set; }
        public TypeRole RoleCurrent { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
    }
}
