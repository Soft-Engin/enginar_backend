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

    public class GetUserByIdDTO
    {
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressName { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int PostCode { get; set; }
        public string RoleName { get; set; }

    }

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
        public byte[]? BannerImage { get; set; }
        public byte[]? ProfileImage { get; set; }

    }

    public class BookmarkRecipesItemDTO
    {
        public string UserName { get; set; }

        public string Header { get; set; }

        public string BodyText { get; set; }
    }

    public class BookmarkBlogsItemDTO
    {
        public string UserName { get; set; }
        public string Header { get; set; }
        public string BodyText { get; set; }
    }

    public class LikedBlogsItemDTO
    {
        public string UserName { get; set; }
        public string Header { get; set; }
        public string BodyText { get; set; }
    }

    public class LikedRecipesItemDTO
    {
        public string UserName { get; set; }
        public string Header { get; set; }
        public string BodyText { get; set; }
    }

    public class UserCompactDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }

}
