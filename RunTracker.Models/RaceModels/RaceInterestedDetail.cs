using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models.RaceModels
{
    public class RaceInterestedDetail
    {
        public int RaceID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        [Display(Name = "Race Name")]
        public string Name { get; set; }

        public string Location { get; set; }

        [Display(Name = "Distance (Miles")]
        public double Distance { get; set; }

        public string Description { get; set; }

        public string Comments { get; set; }

    }
}
