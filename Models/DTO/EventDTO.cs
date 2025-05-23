﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class EventDTO
    {
        public string Title { get; set; }
        public string BodyText { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatorUserName { get; set; } // Username of the creator
        public string CreatorId { get; set; } // Id of the creator
        public Addresses Address { get; set; } // Address details
        public int TotalParticipantsCount { get; set; }

        public List<RequirementDTO> Requirements { get; set; } // List of requirements for the event

        public int EventId { get; set; }
    }

    public class UpdateEventDTO
    {

        public string Title { get; set; }
        public string BodyText { get; set; }
        public DateTime Date { get; set; }
        public int DistrictId { get; set; } // Existing district ID
        public string AddressName { get; set; } // Full address name (e.g., building name or description)
        public string Street { get; set; } // Street details
        public List<int> RequirementIds { get; set; } // List of requirement IDs

    }


    public class CreateEventDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string BodyText { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int DistrictId { get; set; } // Existing district ID

        [Required]
        public string AddressName { get; set; } // Full address name (e.g., building name or description)

        [Required]
        public string Street { get; set; } // Street details

        public List<int> RequirementIds { get; set; }

    }

    public class DistrictDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostCode { get; set; }
    }

    public class CityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EventParticipantsResponseDTO
    {
        public PaginatedResponseDTO<UserCompactDTO> Participations { get; set; }
        public PaginatedResponseDTO<UserCompactDTO>? FollowedParticipations { get; set; }
    }

}
