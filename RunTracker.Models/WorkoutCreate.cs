using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models
{
    public class WorkoutCreate
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double Distance { get; set; }

        [Required]
        public string CompletionTime { get; set; }
        public string Comments { get; set; }
    }
}
