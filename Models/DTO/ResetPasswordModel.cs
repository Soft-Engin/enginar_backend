using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string Token { get; set; }  // This will be the code sent to the user's email
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}

