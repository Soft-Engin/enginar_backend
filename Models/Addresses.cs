using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Addresses
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public Districts District { get; set; }

        [Required]
        public string Street { get; set; }
    }
}
