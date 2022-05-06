using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Beverage_Buddy.Data.Infrastructure;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace Beverage_Buddy.Data.Models
{
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Name", Prompt = "Name")]
        public string Name { get; set; }

        public bool Alcoholic { get; set; }

        [MaxLength(int.MaxValue)]
        public string Instructions { get; set; }

        [Required]
        [Display(Name="Type of Drink")]
        [Range(1, 12, ErrorMessage = "Type of Drink cannot be none.")]
        public DrinkType DrinkType { get; set; }

        [Required]
        [CollectionNotEmpty]
        public ICollection<Ingredient> Ingredients { get; set; }

        public RecipeUser User { get; set; }

        [MaxLength(int.MaxValue)]
        [Display(Name = "Image", Prompt = "Image")]
        public string RecipeThumb { get; set; }
    }
}
