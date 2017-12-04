using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Models;

namespace FitnessTech.Data
{
    public class DBInitializer
    {
        public static void Initialize(FitnessContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Customers.Any() && context.Employees.Any() && context.Exercises.Any() && context.WorkoutTypes.Any())
            {
                return;   // DB has been seeded
            }

            var customers = new Customer[]
            {
            new Customer{FirstName = "Stella", LastName = "Selena", Birthday = DateTime.Parse("1991-10-28"), Gender = Gender.Female, Country = "Norway", City = "Oslo", Email = "sathie@live.com", Goal = Goal.Maintenance, Weight = 65, Height = 175, ActivityLevel = ActivityLevel.Moderate, BodyFat = 20},
            new Customer{FirstName = "Stefan", LastName = "Selena", Birthday = DateTime.Parse("1991-10-28"), Gender = Gender.Female, Country = "Norway", City = "Oslo", Email = "sathie@live.com", Goal = Goal.Maintenance, Weight = 65, Height = 175, ActivityLevel = ActivityLevel.Moderate, BodyFat = 20},
            new Customer{FirstName = "Athena", LastName = "Selena", Birthday = DateTime.Parse("1991-10-28"), Gender = Gender.Female, Country = "Norway", City = "Oslo", Email = "sathie@live.com", Goal = Goal.Maintenance, Weight = 65, Height = 175, ActivityLevel = ActivityLevel.Moderate, BodyFat = 20}
            
            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }

            context.SaveChanges();

            var employees = new Employee[]
            {
                new Employee{FirstName = "Layne", LastName = "Norton", Birthday = DateTime.Parse("1991-10-28"), Gender = Gender.Female, Country = "Norway", City = "Oslo", Email = "sathie@live.com", Certification = Certification.PersonalTrainer, Specialization = Specialization.SportsConditioning, Gym = new Gym{GymName = "Myrens", Email = "myrens@live.no"}},
                new Employee{FirstName = "Mark", LastName = "Bell",  Birthday = DateTime.Parse("1991-10-28"), Gender = Gender.Female, Country = "Norway", City = "Oslo", Email = "sathie@live.com", Certification = Certification.PersonalTrainer, Specialization = Specialization.SportsConditioning, Gym = new Gym{GymName = "Myrens", Email = "myrens@live.no"}},
                new Employee{FirstName = "Omar", LastName = "Isuf", Birthday = DateTime.Parse("1991-10-28"), Gender = Gender.Female, Country = "Norway", City = "Oslo", Email = "sathie@live.com", Certification = Certification.PersonalTrainer, Specialization = Specialization.SportsConditioning, Gym = new Gym{GymName = "Myrens", Email = "myrens@live.no"}}


            };
            foreach (Employee e in employees)
            {
                context.Employees.Add(e);
            }
            context.SaveChanges();

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
                context.Exercises.Add(e);
            }
            context.SaveChanges();



            var workoutTypes = new WorkoutType[]
            {
                new WorkoutType{WorkoutTypeName = "Strength", WorkoutTypeDescription = "Strength training"},
                new WorkoutType{WorkoutTypeName = "Cardio", WorkoutTypeDescription = "Cardio training"},
                new WorkoutType{WorkoutTypeName = "HIIT", WorkoutTypeDescription = "High intensity intervals training"}
               
            };
            foreach (WorkoutType w in workoutTypes)
            {
                context.WorkoutTypes.Add(w);
            }

            context.SaveChanges();

            var Workouts = new Workout[]
            {
                new Workout{WorkoutName = "PHAT", WorkoutType = workoutTypes.SingleOrDefault(w => w.WorkoutTypeId == 1)
                },
                new Workout{WorkoutName = "Starting Strength", WorkoutType = workoutTypes.SingleOrDefault(w => w.WorkoutTypeId == 2)
                }

            };
            foreach (Workout workout in Workouts)
            {
                context.Workouts.Add(workout);
            }

            context.SaveChanges();


        }
    }
}
