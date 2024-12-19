using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class BlogDTO
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string BodyText { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int? RecipeId { get; set; }
    }

    public class BlogDetailDTO
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string BodyText { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int? RecipeId { get; set; }
        public string RecipeHeader { get; set; }
        public RecipeDetailsDTO Recipe { get; set; } // Full recipe details
    }


    public class CreateBlogDTO
    {
        public string Header { get; set; }
        public string BodyText { get; set; }
        public int? RecipeId { get; set; } // Optional: Associate an existing recipe
        public CreateRecipeDTO? Recipe { get; set; } // Optional: Create a new recipe
    }

    public class UpdateBlogDTO
    {
        public string Header { get; set; }
        public string BodyText { get; set; }
        public int? RecipeId { get; set; } // To associate or disassociate a recipe
        public RecipeRequestDTO Recipe { get; set; } // For updating or creating a recipe
    }

}
