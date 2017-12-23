using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Data.Helpers
{
    public enum Specialization
    {
        [Display(Name = "Sports Conditioning")]
        SportsConditioning,
        [Display(Name = "Senior Fitness")]
        SeniorFitness,
        [Display(Name = "Youth Fitness")]
        YouthFitness,
        [Display(Name = "Weight Management")]
        WeightManagement

    }
}
