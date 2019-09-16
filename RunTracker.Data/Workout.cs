using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Data
{
    public class Workout
    {
        [Key]
        public int ID { get; set; }


        //public int ShoeID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name ="Distance(Miles)")]
        public double Distance { get; set; }

        public string CompletionTime { get; set; }

        public string Comments { get; set; }

        [Required]
        public Guid UserID { get; set; }
    }
}
