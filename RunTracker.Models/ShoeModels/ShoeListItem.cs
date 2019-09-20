using RunTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models
{
    public class ShoeListItem
    {
        public int ShoeID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Workout> Workouts { get; set; }

        public double? MilesRun
        {
            get
            {
                double totalMiles = 0;
                foreach (var workout in Workouts)
                {
                    totalMiles += workout.Distance;
                }
                return totalMiles;
            }
        }
    }
}
