using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BookMessenger.Models
{
    public enum TypeRole
    {
        [Display(Name = "Без роли")]
        None = 0,
        [Display(Name = "Администратор")]
        Admin = 1,
        [Display(Name = "Оператор")]
        Operator = 2,
        [Display(Name = "Пользователь")]
        User = 4
    }
    public class User
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public TypeRole? Role { get; set; }  = TypeRole.None;
        public UserProfile? UserProfile { get; set; }

    }
}
