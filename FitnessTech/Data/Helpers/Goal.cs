using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Data.Helpers
{
    public enum Goal
    {
        Weightloss,
        Maintenance,
        [Display(Name = "Weight gain")]
        WeightGain,
        [Display(Name = "Muscle gain")]
        Musclegain
    }
}
