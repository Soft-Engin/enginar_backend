using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Recipes_Ingredients
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IngredientId { get; set; }
        [ForeignKey("IngredientId")]
        public Ingredients Ingredient { get; set; }

        [Required]
        public int RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipes Recipe { get; set; }
    }
}
