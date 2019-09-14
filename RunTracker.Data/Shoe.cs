using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Data
{
    public class Shoe
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid UserID { get; set; }

        //public double? MilesRun { get; set; }

        // maybe add nickname or type (ex. "racing flat")
    }
}
