using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string BodyText { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        //public byte[][]? Images { get; set; }
        public int ServingSize { get; set; }
        public int PreparationTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class RecipeDetailsDTO
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string BodyText { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int ServingSize { get; set; }
        public int PreparationTime { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<RecipeIngredientDetailsDTO> Ingredients { get; set; }
    }

    public class CreateRecipeDTO
    {
        public string Header { get; set; }
        public string BodyText { get; set; }
        public byte[]? BannerImage{ get; set; }
        public byte[][]? StepImages { get; set; }
        public int ServingSize { get; set; }
        public int PreparationTime { get; set; }
        public List<RecipeIngredientRequestDTO> Ingredients { get; set; }
    }

    public class RecipeIngredientRequestDTO
    {
        public int IngredientId { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
    }

    public class RecipeIngredientDetailsDTO : RecipeIngredientRequestDTO
    {
        public string IngredientName { get; set; }
    }

    // Used at requesting to create or update a recipe.
    // So it is logical to get images through here
    public class RecipeRequestDTO
    {
        [Required]
        public string Header { get; set; }
        [Required]
        public string BodyText { get; set; }
        public byte[]? BannerImage { get; set; }
        public byte[][]? StepImages { get; set; }
        public int ServingSize { get; set; }
        public int PreparationTime { get; set; }
        public List<RecipeIngredientRequestDTO> Ingredients { get; set; }
    }
}
