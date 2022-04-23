using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beverage_Buddy.Data.Models
{
    public class DrinkIngredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Measure { get; set; }
        [Required]
        public string DrinkId { get; set; }
    }
}
