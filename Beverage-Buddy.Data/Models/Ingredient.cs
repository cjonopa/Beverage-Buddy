using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Beverage_Buddy.Data.Models
{
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public string Amount { get; set; }

        public int RecipeId { get; set; }

        public string Item => $"{Amount} - {Name}";
    }
}
