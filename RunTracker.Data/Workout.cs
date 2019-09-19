using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Data
{
    public class Workout
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Shoe")]
        public int ShoeID { get; set; }
        public virtual Shoe Shoe { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name ="Distance(Miles)")]
        public double Distance { get; set; }

        [Display(Name ="Completion Time")]
        public string CompletionTime { get; set; }

        public string Comments { get; set; }

        [Required]
        public Guid UserID { get; set; }
    }
}
