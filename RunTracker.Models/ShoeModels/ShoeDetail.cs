﻿using RunTracker.Data;
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

        [Display(Name ="Miles Run")] 
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
        //public double? MilesRun { get; set; }
    }
}
