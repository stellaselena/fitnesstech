using System.ComponentModel.DataAnnotations;

namespace FitnessTech.Data.Entities
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
