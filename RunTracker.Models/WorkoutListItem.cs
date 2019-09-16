using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models
{
    public class WorkoutListItem
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public double Distance { get; set; }
    }
}
