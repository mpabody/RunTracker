using RunTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models.RaceModels
{
    public class RaceRanEdit
    {
        public int RaceID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Race Name")]
        public string Name { get; set; }

        public string Location { get; set; }

        [Display(Name = "Distance (Miles)")]
        public double Distance { get; set; }

        public string Description { get; set; }

        public string Comments { get; set; }

        [Display(Name ="Completion Time")]
        public string CompletionTime { get; set; }

        [ForeignKey("Shoe")]
        public int? ShoeID { get; set; }
        public virtual Shoe Shoe { get; set; }
    }
}
