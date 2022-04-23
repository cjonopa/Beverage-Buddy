using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beverage_Buddy.Data.Models
{
    public class Drink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        [Required]
        [MaxLength(255)]
        [Display(Name = "Drink Name")]
        public string DrinkName { get; set; }
        [Display(Name = "Drink Alternate")]
        public string DrinkAlternate { get; set; }
        public string Tags { get; set; }
        public string Video { get; set; }
        public string Category { get; set; }
        public string IBA { get; set; }
        public bool Alcoholic { get; set; }
        public string Glass { get; set; }
        public string Instructions { get; set; }
        public string DrinkThumb { get; set; }
        [Display(Name = "Drink Ingredients")]
        public List<DrinkIngredient> DrinkIngredients { get; set; }
        public string ImageSource { get; set; }
        public string ImageAttribution { get; set; }
        public bool CreativeCommonsConfirmed { get; set; }
        public DateTime? Modified { get; set; }

        public Drink()
        {
            DrinkIngredients = new List<DrinkIngredient>();
        }
    }
}
