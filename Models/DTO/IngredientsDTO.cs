using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class IngredientIdDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
    }

    public class IngredientDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int TypeId { get; set; }
    }
}
