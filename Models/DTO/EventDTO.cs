using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class EventDto
    {
        public string Title { get; set; }
        public string BodyText { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatorUserName { get; set; } // Username of the creator
        public Addresses Address { get; set; } // Address details
        public List<ParticipantDto> Participants { get; set; }
        public int TotalParticipantsCount { get; set; }

        public List<RequirementDto> Requirements { get; set; } // List of requirements for the event

        public int EventId { get; set; }
    }

    public class UpdateEventDto
    {
        
        public string Title { get; set; }
        public string BodyText { get; set; }
        public DateTime Date { get; set; }
        public int DistrictId { get; set; } // Existing district ID
        public string AddressName { get; set; } // Full address name (e.g., building name or description)
        public string Street { get; set; } // Street details
        public List<int> RequirementIds { get; set; } // List of requirement IDs

    }


    public class CreateEventDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string BodyText { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int DistrictId { get; set; } // Existing district ID

        [Required]
        public string AddressName { get; set; } // Full address name (e.g., building name or description)

        [Required]
        public string Street { get; set; } // Street details

        public List<int> RequirementIds { get; set; }

    }

    public class ParticipantDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }
    }



}
