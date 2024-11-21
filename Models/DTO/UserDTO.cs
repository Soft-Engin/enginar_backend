using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class UserDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Addresses? Address { get; set; }
        public Roles Role { get; set; }
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
