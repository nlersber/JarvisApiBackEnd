using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JarvisNG.DTO {
    public class RegisterDTO {
        [Required]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Username must be at least 5 characters long")]
        public string Username { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("[a-zA-z0-9].{6,}$", ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }
    }
}

