using System;
using System.ComponentModel.DataAnnotations;

namespace MyProperty.Data.Entities
{
    public class Owner : BaseEntity
    {
        [Key]
        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

    }
}
