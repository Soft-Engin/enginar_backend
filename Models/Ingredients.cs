using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Ingredients
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public IngredientTypes Type { get; set; }
        public byte[]? Image { get; set; }

        // Navigation property for many-to-many relationship with Preferences (Allergens)
        public ICollection<Ingredients_Preferences> Ingredients_Preferences { get; set; }
    }
}
