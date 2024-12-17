using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Users_Blogs_Interaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }

        [Required]
        public int BlogId { get; set; }
        [ForeignKey("BlogId")]
        public Blogs Blog { get; set; }

        [Required]
        public int InteractionId { get; set; }
        [ForeignKey("InteractionId")]
        public Interactions Interaction { get; set; }
    }
}
