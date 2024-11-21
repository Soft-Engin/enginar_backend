using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class UpdateUserDto
    {
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AddressName { get; set; }
        public string? Street { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public int PostCode { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
