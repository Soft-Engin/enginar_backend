using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Blogs
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }

        [Required]
        public string Header { get; set; }

        [Required]
        public string BodyText { get; set; }

        public int? RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipes? Recipe { get; set; }
    }
}
