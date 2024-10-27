using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Districts
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public Cities City { get; set; }

        [Required]
        public int PostCode { get; set; }
    }
}
