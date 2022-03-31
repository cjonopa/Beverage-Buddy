﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beverage_Buddy.Data.Entities
{
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public bool Alcoholic { get; set; }

        [Required]
        [Display(Name="Type of Drink")]
        public DrinkType DrinkType { get; set; }

        public IEnumerable<RecipeIngredient> Ingredients { get; set; }
    }
}