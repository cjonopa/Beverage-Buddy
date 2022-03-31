using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beverage_Buddy.Web.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "Name is too short.")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Message is too long.")]
        public string Message { get; set; }
    }
}
