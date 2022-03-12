using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeverageBuddy.Data.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public bool Alcoholic { get; set; }

        [Required]
        [Display(Name="Type of Drink")]
        public DrinkType DrinkType { get; set; }

        public IEnumerable<string> Ingredients { get; set; }
    }
}
