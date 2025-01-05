using Microsoft.EntityFrameworkCore;
using Models.InteractionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class RecipeInteractionDefaultData
    {
        public void PopulateRecipeCommentsData(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Recipe_Comments>().HasData(

            //);
        }

        public void PopulateRecipeLikesData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe_Likes>().HasData(
            new Recipe_Likes { Id = 1, UserId = "5", RecipeId = 2, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 2, UserId = "5", RecipeId = 3, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 3, UserId = "6", RecipeId = 1, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 4, UserId = "6", RecipeId = 5, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 5, UserId = "7", RecipeId = 5, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 6, UserId = "8", RecipeId = 1, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 7, UserId = "9", RecipeId = 3, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 8, UserId = "9", RecipeId = 5, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 9, UserId = "10", RecipeId = 1, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 10, UserId = "13", RecipeId = 3, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 11, UserId = "14", RecipeId = 2, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 12, UserId = "21", RecipeId = 1, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 13, UserId = "21", RecipeId = 2, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 14, UserId = "21", RecipeId = 4, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 15, UserId = "24", RecipeId = 3, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 16, UserId = "29", RecipeId = 5, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 17, UserId = "31", RecipeId = 2, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 18, UserId = "33", RecipeId = 3, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 19, UserId = "41", RecipeId = 2, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 20, UserId = "41", RecipeId = 4, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 21, UserId = "42", RecipeId = 1, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 22, UserId = "44", RecipeId = 4, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 23, UserId = "46", RecipeId = 5, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 24, UserId = "52", RecipeId = 5, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 25, UserId = "53", RecipeId = 5, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 26, UserId = "55", RecipeId = 1, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 27, UserId = "57", RecipeId = 3, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 28, UserId = "57", RecipeId = 5, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 29, UserId = "58", RecipeId = 3, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 30, UserId = "59", RecipeId = 3, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 31, UserId = "61", RecipeId = 5, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 32, UserId = "64", RecipeId = 2, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 33, UserId = "68", RecipeId = 1, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 34, UserId = "68", RecipeId = 2, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 35, UserId = "68", RecipeId = 3, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 36, UserId = "69", RecipeId = 3, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 37, UserId = "70", RecipeId = 5, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 38, UserId = "74", RecipeId = 3, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 39, UserId = "76", RecipeId = 1, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 40, UserId = "77", RecipeId = 1, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 41, UserId = "78", RecipeId = 1, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 42, UserId = "81", RecipeId = 4, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 43, UserId = "83", RecipeId = 3, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 44, UserId = "83", RecipeId = 5, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 45, UserId = "86", RecipeId = 3, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 46, UserId = "90", RecipeId = 1, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 47, UserId = "91", RecipeId = 3, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 48, UserId = "93", RecipeId = 5, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 49, UserId = "96", RecipeId = 3, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 50, UserId = "97", RecipeId = 3, CreatedAt = DateTime.UtcNow },
            new Recipe_Likes { Id = 51, UserId = "99", RecipeId = 2, CreatedAt = DateTime.UtcNow }
        );
        }

    }
}
