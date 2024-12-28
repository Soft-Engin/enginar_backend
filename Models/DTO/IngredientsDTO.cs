using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class IngredientIdDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // IngredientType information
        public IngredientTypeIdDTO Type { get; set; }

        // List of allergens (preferences) associated with the ingredient
        public List<AllergenIdDTO> Allergens { get; set; }
    }

    public class IngredientDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int TypeId { get; set; }
        public byte[]? Image { get; set; }

        // List of allergen IDs associated with the ingredient
        public List<int> AllergenIds { get; set; }
    }

    public class IngredientImageDTO
    {
        public int Id { get; set; }
        public byte[]? Image { get; set; }
    }
}
