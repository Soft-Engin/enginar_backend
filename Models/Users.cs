using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Users : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }


        public int? AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Addresses? Address { get; set; }


        [Required]
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Roles Role { get; set; }

        public byte[]? BannerImage { get; set; }
        public byte[]? ProfileImage { get; set; }

        public string? Bio {  get; set; }
    }
}
