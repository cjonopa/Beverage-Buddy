using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageBuddy.Web.Models
{
    public class ContactViewModel
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Subject is too long.")]
        public string Subject { get; set; }
        [Required]
        [MaxLength(2500, ErrorMessage = "Message is too long.")]
        public string Message { get; set; }
    }
}
