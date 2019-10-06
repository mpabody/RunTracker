using RunTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models
{
    public class ShoeDetail
    {
        public int ShoeID { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Workout> Workouts { get; set; }
        public virtual ICollection<Race> Races { get; set; }


        [Display(Name ="Miles Run")] 
        public double? MilesRun
        {
            get
            {
                double workoutMiles = 0;
                foreach (var workout in Workouts)
                {
                    workoutMiles += workout.Distance;
                }

                double raceMiles = 0;
                foreach (var race in Races)
                {
                    if (race.CompletionTime != null)
                    {
                        raceMiles += race.Distance;
                    }
                }

                return workoutMiles + raceMiles;
            }
        }

        public string Description { get; set; }
    }
}
