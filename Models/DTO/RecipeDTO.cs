using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string BodyText { get; set; }
    }

    public class RecipeDetailsDTO
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string BodyText { get; set; }

        public List<RecipeIngredientDetailsDTO> Ingredients { get; set; }
    }

    public class CreateRecipeDTO
    {
        public string Header { get; set; }
        public string BodyText { get; set; }

        public string UserId { get; set; }

        public List<RecipeIngredientDetailsDTO> Ingredients { get; set; }
    }

    public class RecipeIngredientDetailsDTO
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
    }

    public class RecipeRequestDTO
    {
        [Required]
        public string Header { get; set; }
        [Required]
        public string BodyText { get; set; }
        public List<RecipeIngredientDetailsDTO> Ingredients { get; set; }

    }
}
