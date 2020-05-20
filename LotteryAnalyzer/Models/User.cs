using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LotteryAnalyzer.Models
{
    public class User
    {
        [Key]
        public Guid? UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        public string AccessToken { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public string NewPassword { get; set; }

    }
}
