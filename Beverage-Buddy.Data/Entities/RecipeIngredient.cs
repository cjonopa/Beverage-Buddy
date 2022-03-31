using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beverage_Buddy.Data.Entities
{
    public class RecipeIngredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int IngredientId { get; set; }

        [Required]
        public int RecipeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Amount { get; set; }
    }
}
