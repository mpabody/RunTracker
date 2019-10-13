using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models.RaceModels
{
    public class RaceListItem
    {
        public int RaceID { get; set; }

        [Display(Name = "Race Name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        public string Location { get; set; }

        [Display(Name = "Distance (Miles)")]
        public double Distance { get; set; }

        // Needed for AllRaces Index
        [Display(Name = "Completion Time")]
        public string CompletionTime { get; set; }
    }
}
