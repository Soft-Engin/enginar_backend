using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class IngredientTypeDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }

    public class IngredientTypeIdDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
