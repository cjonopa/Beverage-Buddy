using System.Collections.Generic;

namespace BeverageBuddy.Data.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Alcoholic { get; set; }
        public DrinkType DrinkType { get; set; }
        public IEnumerable<string> Ingredients { get; set; }
    }
}
