using FitnessTech.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessTech.ViewModels
{
    public class EmployeeIndexDataViewModel
    {

        public EmployeeIndexDataViewModel()
        {
            Employees = new List<Employee>();
        }

        public IEnumerable<Employee> Employees { get; set; }
        [Display(Name = "Sports Conditioning")]
        public bool SportsConditioning { get; set; }
        [Display(Name = "Senior Fitness")]
        public bool SeniorFitness { get; set; }
        [Display(Name = "Youth Fitness")]
        public bool YouthFitness { get; set; }
        [Display(Name = "Weight Management")]
        public bool WeightManagement { get; set; }
        [Display(Name = "Personal Trainer")]
        public bool PersonalTrainer{ get; set; }
        [Display(Name = "Nutritionist")]
        public bool Nutritionist{ get; set; }
    }
}
