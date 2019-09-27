using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Data
{
    public class Race
    {
        [Key]
        public int RaceID { get; set; }

        [ForeignKey ("Shoe")]
        public int? ShoeID { get; set; }
        public virtual Shoe Shoe { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name ="Race Name")]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [Display(Name ="Distance (Miles)")]
        public double Distance { get; set; }

        public string Description { get; set; }

        public string Comments { get; set; }

        public string CompletionTime { get; set; }

        public Guid UserID { get; set; }
    }
}
