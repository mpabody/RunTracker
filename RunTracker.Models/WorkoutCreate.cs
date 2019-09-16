﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models
{
    public class WorkoutCreate
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double Distance { get; set; }

        [Required]
        public string CompletionTime { get; set; }

        [MinLength(2, ErrorMessage = "Please enter at leasst 2 characters.")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        public string Comments { get; set; }
    }
}