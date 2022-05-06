using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beverage_Buddy.Data.Models;

namespace Beverage_Buddy.Data.Infrastructure
{
    public class CollectionNotEmpty : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (Recipe) validationContext.ObjectInstance;
            return Extension.IsNullOrEmpty(model.Ingredients) 
                ? new ValidationResult("Collection cannot be empty.") 
                : ValidationResult.Success;
        }
    }
}
