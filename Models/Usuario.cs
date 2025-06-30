using System.ComponentModel.DataAnnotations;

namespace MVCClasico.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsAdmin => !string.IsNullOrEmpty(Email) && Email.EndsWith("@admin.com");
    }
} 