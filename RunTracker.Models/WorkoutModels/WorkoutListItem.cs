using RunTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models
{
    public class WorkoutListItem
    {
        public int WorkoutID { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name ="Distance (Miles)")]
        public double Distance { get; set; }

        [ForeignKey("Shoe")]
        public int? ShoeID { get; set; }
        public virtual Shoe Shoe { get; set; }
    }
}
