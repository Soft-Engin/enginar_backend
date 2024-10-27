using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Events
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public Users Creator { get; set; }

        [Required]
        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Addresses Address { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string BodyText { get; set; }

        
    }
}
