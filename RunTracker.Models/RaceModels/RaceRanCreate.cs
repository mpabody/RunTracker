using RunTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models.RaceModels
{
    public class RaceRanCreate
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Race Name")]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Distance (Miles)")]
        public double Distance { get; set; }

        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        public string Description { get; set; }

        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        public string Comments { get; set; }

        [Required]
        [Display(Name = "Completion Time")]
        public string CompletionTime { get; set; }

        [ForeignKey("Shoe")]
        public int? ShoeID { get; set; }
        public virtual Shoe Shoe { get; set; }
    }
}
