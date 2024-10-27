using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Users_Interactions
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int InitiatorUserId { get; set; }  
        [ForeignKey("InitiatorUserId")]
        public Users InitiatorUser { get; set; } 

        [Required]
        public int TargetUserId { get; set; }  
        [ForeignKey("TargetUserId")]  
        public Users TargetUser { get; set; } 

        [Required]
        public int InteractionId { get; set; }
        [ForeignKey("InteractionId")]
        public Interactions Interaction { get; set; }

    }
}
