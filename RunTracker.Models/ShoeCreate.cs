using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models
{
    public class ShoeCreate
    {
        [Required]
        [MinLength(3, ErrorMessage = "Please enter at least 3 characters.")]
        [MaxLength(20, ErrorMessage = "There are too many characters in this field.")]
        public string Brand { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Please enter at leaset 3 characters.")]
        [MaxLength(20, ErrorMessage = "There are too many characters in this field.")]
        public string Name { get; set; }
    }
}
