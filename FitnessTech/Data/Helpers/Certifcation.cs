using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Data.Helpers
{
    public enum Certification
    {
        [Display(Name = "Personal Trainer")]
        PersonalTrainer,
        [Display(Name = "Nutritionist")]
        Nutritionist
    }

}
