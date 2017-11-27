using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        [Display(Name = "Recipe name ")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string RecipeName { get; set; }
        [Display(Name = "Recipe description ")]
        public int RecipeDescription { get; set; }
    }
}
