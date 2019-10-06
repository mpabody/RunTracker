using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models
{
    public class ShoeEdit
    {
        public int ShoeID { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Please enter at least 3 characters.")]
        [MaxLength(20, ErrorMessage = "There are too many characters in this field.")]
        public string Brand { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Please enter at leaset 3 characters.")]
        [MaxLength(40, ErrorMessage = "There are too many characters in this field.")]
        public string Name { get; set; }

        [MinLength(3, ErrorMessage = "Please enter at leaset 3 characters.")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        public string Description { get; set; }
    }
}
