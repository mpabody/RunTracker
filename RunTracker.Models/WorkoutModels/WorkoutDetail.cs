﻿using RunTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models
{
    public class WorkoutDetail
    {
        public int WorkoutID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public double Distance { get; set; }

        [ForeignKey("Shoe")]
        public int ShoeID { get; set; }
        public virtual Shoe Shoe { get; set; }

        [Display(Name ="Completion Time")]
        public string CompletionTime { get; set; }
        public string Comments { get; set; }
    }
}