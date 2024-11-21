using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Recipes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }

        [Required]
        public string Header { get; set; }

        [Required]
        public string BodyText { get; set; }

        // Navigation property for Recipes_Ingredients
        public ICollection<Recipes_Ingredients> Recipes_Ingredients { get; set; }
    }
}
