using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Ingredients_Preferences
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IngredientId { get; set; }
        [ForeignKey("IngredientId")]
        public Ingredients Ingredient { get; set; }

        [Required]
        public int PreferenceId { get; set; }
        [ForeignKey("PreferenceId")]
        public Preferences Preference { get; set; }
    }
}
