﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Recipes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }

        [Required]
        public string Header { get; set; }

        [Required]
        public string BodyText { get; set; }
        public byte[]? BannerImage { get; set; }
        public byte[][]? StepImages { get; set; }

        [Required]
        public int ServingSize { get; set; }

        [Required]
        public int PreparationTime { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string[] Steps { get; set; }

        // Navigation property for Recipes_Ingredients
        public ICollection<Recipes_Ingredients> Recipes_Ingredients { get; set; }
    }
}
