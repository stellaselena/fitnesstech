using FitnessTech.Data.Entities;
using FitnessTech.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Data
{
    public class FitnessSeeder
    {
        private readonly FitnessContext _context;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<User> _userManager;

        public FitnessSeeder(FitnessContext context, IHostingEnvironment hosting, UserManager<User> userManager)
        {
            _context = context;
            _hosting = hosting;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            _context.Database.EnsureCreated();
            var user = await _userManager.FindByEmailAsync("sathie@live.com");
            if (user == null)
            {
                user = new User()
                {
                    FirstName = "Stella",
                    LastName = "Selena",
                    UserName = "sathie@live.com",
                    Email = "sathie@live.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default user");
                }
            }
            if (!_context.Products.Any())
            {
                //create sample data from json
                var file = Path.Combine(_hosting.ContentRootPath, "Data/supplement.json");
                var json = File.ReadAllText(file);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _context.Products.AddRange(products);

                var order = new Order()
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "12345",
                    User = user,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };

                _context.Orders.Add(order);
            }

            if (!_context.Gyms.Any())
            {
                var gyms = new Gym[]
                {
                    new Gym {GymName = "Myrens", City = "Oslo", Country = "Norway", PostalCode = 0473, Description = "Myrens squash og treningssenter", Street = "Sandakerveien 22", Website = "www.myrens.no", Email = "myrens@live.com", PhoneNumber = 94815633},
                    new Gym {GymName = "Sats", City = "Oslo", Country = "Norway", PostalCode = 0372, Description = "Sats treningssenter", Street = "Arendalsgata 33", Website = "www.sats.no", Email = "sats@live.com", PhoneNumber = 45633943},
                    new Gym {GymName = "Elixia", City = "Oslo", Country = "Norway", PostalCode = 0252, Description = "Elixia treningssenter", Street = "Middelthunsgate 15", Website = "www.elixia.no", Email = "elixia@live.com", PhoneNumber = 4335442}
                };
                foreach (Gym g in gyms)
                {
                    _context.Gyms.Add(g);
                }
            }
            if (!_context.Employees.Any())
            {
                var employees = new Employee[]
                {
                    new Employee{FirstName = "Layne", LastName = "Norton", Birthday = DateTime.Parse("1991-10-28"), Gender = Gender.Male, Country = "Norway", City = "Oslo", Email = "layne@live.com", Certification = Certification.PersonalTrainer, Specialization = Specialization.SportsConditioning, /*GymId = 1*/},
                    new Employee{FirstName = "Mark", LastName = "Bell",  Birthday = DateTime.Parse("1991-10-28"), Gender = Gender.Male, Country = "Norway", City = "Oslo", Email = "mark@live.com", Certification = Certification.PersonalTrainer, Specialization = Specialization.SportsConditioning, /*GymId = 2*/},
                    new Employee{FirstName = "Omar", LastName = "Isuf", Birthday = DateTime.Parse("1991-10-28"), Gender = Gender.Male, Country = "Norway", City = "Oslo", Email = "omar@live.com", Certification = Certification.PersonalTrainer, Specialization = Specialization.SportsConditioning, /*GymId = 3*/}


                };
                foreach (Employee e in employees)
                {
                    _context.Employees.Add(e);
                }
            }

            if (!_context.Customers.Any())
            {
                var customers = new Customer[]
                {
                    new Customer{FirstName = "Stella", LastName = "Selena", Birthday = DateTime.Parse("1991-10-28"), Gender = Gender.Female, Country = "Norway", City = "Oslo", Email = "sathie@live.com", Goal = Goal.Maintenance, Weight = 65, Height = 175, ActivityLevel = ActivityLevel.Moderate, BodyFat = 20/*, GymId = 1, EmployeeId = 1*/},
                    new Customer{FirstName = "Stefan", LastName = "Selena", Birthday = DateTime.Parse("1991-10-28"), Gender = Gender.Female, Country = "Norway", City = "Oslo", Email = "sathie@live.com", Goal = Goal.Maintenance, Weight = 65, Height = 175, ActivityLevel = ActivityLevel.Moderate, BodyFat = 20/*, GymId = 2/*, EmployeeId = 2*/},
                    new Customer{FirstName = "Athena", LastName = "Selena", Birthday = DateTime.Parse("1991-10-28"), Gender = Gender.Female, Country = "Norway", City = "Oslo", Email = "sathie@live.com", Goal = Goal.Maintenance, Weight = 65, Height = 175, ActivityLevel = ActivityLevel.Moderate, BodyFat = 20/*, GymId = 3/*, EmployeeId = 3*/}

                };
                foreach (Customer c in customers)
                {
                    _context.Customers.Add(c);
                }

            }

            if (!_context.Exercises.Any())
            {
                var exercises = new Exercise[]
                {
                    new Exercise
                    {
                        ExerciseName = "Squat",
                        ExerciseDescription = "squat down",
                        MuscleGroup = MuscleGroup.Legs
                    },
                    new Exercise
                    {
                        ExerciseName = "Rows",
                        ExerciseDescription = "barbell/ dumbbell",
                        MuscleGroup = MuscleGroup.Back
                    },
                    new Exercise
                    {
                        ExerciseName = "Shoulder press",
                        ExerciseDescription = "alternative to barbell press",
                        MuscleGroup = MuscleGroup.Shoulders
                    }
                };
                foreach (Exercise e in exercises)
                {
                    _context.Exercises.Add(e);
                }

            }
            if (!_context.WorkoutTypes.Any())
            {
                var workoutTypes = new WorkoutType[]
                {
                    new WorkoutType{WorkoutTypeName = "Strength", WorkoutTypeDescription = "Strength training"},
                    new WorkoutType{WorkoutTypeName = "Cardio", WorkoutTypeDescription = "Cardio training"},
                    new WorkoutType{WorkoutTypeName = "HIIT", WorkoutTypeDescription = "High intensity intervals training"}

                };
                foreach (WorkoutType w in workoutTypes)
                {
                    _context.WorkoutTypes.Add(w);
                }
            }
            if (!_context.Workouts.Any())
            {
                var workouts = new Workout[]
                {
                    new Workout{WorkoutName = "Legs",/* WorkoutTypeId = 1*/},
                    new Workout{WorkoutName = "Chest and Shoulders"/*, WorkoutTypeId = 1*/}

                };
                foreach (Workout workout in workouts)
                {
                    _context.Workouts.Add(workout);
                }
            }
            if (!_context.WorkoutPrograms.Any())
            {
                var workoutPrograms = new WorkoutProgram[]
                {
                    new WorkoutProgram{WorkoutProgramName = "PHAT", WorkoutProgramDescription = "Layne Norton's Program"},
                    new WorkoutProgram{WorkoutProgramName = "Starting Strength", WorkoutProgramDescription = "Mark Rippetoe's Program"}
                };

                foreach (WorkoutProgram workoutProgram in workoutPrograms)
                {
                    _context.WorkoutPrograms.Add(workoutProgram);
                }

            }

            //if (!_context.ExerciseAssigments.Any())
            //{
            //    var exerciseAssigments = new ExerciseAssigment[]
            //    {
            //        new ExerciseAssigment{WorkoutId = _context.Workouts.First().WorkoutId, ExerciseId = _context.Exercises.First().ExerciseId},

            //    };

            //    foreach (ExerciseAssigment exerciseAssigment in exerciseAssigments)
            //    {
            //        _context.ExerciseAssigments.Add(exerciseAssigment);
            //    }

            //}


            //if (!_context.WorkoutAssigments.Any())
            //{
            //    var workoutAssigments = new WorkoutAssigment[]
            //    {
            //        new WorkoutAssigment{WorkoutId = _context.Workouts.First().WorkoutId, WorkoutProgramId = _context.WorkoutPrograms.First().WorkoutProgramId}
            //    };

            //    foreach (WorkoutAssigment workoutAssigment in workoutAssigments)
            //    {
            //        _context.WorkoutAssigments.Add(workoutAssigment);
            //    }

            //}

            _context.SaveChanges();

        }
    }
}
