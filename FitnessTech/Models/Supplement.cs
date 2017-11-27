using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Models
{
    public enum SupplementType
    {
        Multivitamin,
        Digestion,
        WeightManagement,
        SportsNutrition,
        Superfood

    }
    public class Supplement
    {
        [Key]
        public int SupplementId { get; set; }

        [Display(Name = "Supplement Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public int SupplementName { get; set; }

        [Display(Name = "Supplement Description")]
        public int SupplementDescription { get; set; }

        [Display(Name = "Supplement Type")]
        public SupplementType SupplementType { get; set; }
    }
}
