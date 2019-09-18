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
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }

        [Display(Name ="Miles Run")]
        public double? MilesRun { get; set; }
    }
}
