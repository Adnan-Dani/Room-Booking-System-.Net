using System.ComponentModel.DataAnnotations;

namespace RMS.Models
{
    public class Users
    {
        // making primary key
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string Role { get; set; } = "user";

    }
}
