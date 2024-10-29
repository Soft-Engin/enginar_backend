using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Events_Requirements
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Events Event { get; set; }

        [Required]
        public int RequirementId { get; set; }
        [ForeignKey("RequirementId")]
        public Requirements Requirement { get; set; }
    }
}
