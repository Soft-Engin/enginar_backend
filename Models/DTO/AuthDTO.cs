using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class ForgotPasswordDTO
    {
        [EmailAddress]
        public string Email { get; set; }
    }

    public class LoginRequestDTO
    {
        [Required]
        public string Identifier { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class MakeAdminDTO
    {
        [Required]
        public string UserName { get; set; }
    }

    public class RegisterRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }

    public class ResetPasswordDTO
    {
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }  // This will be the code sent to the user's email
        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

