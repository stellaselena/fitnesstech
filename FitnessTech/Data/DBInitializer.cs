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

            if (context.Customers.Any() && context.Employees.Any() && context.Exercises.Any() && context.WorkoutTypes.Any() && context.Workouts.Any() && context.WorkoutPrograms.Any()
                && context.ExerciseAssigments.Any() && context.WorkoutAssigments.Any())
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

            var gyms = new Gym[]
            {
                new Gym {GymName = "Myrens"},
                new Gym {GymName = "Sats"},
                new Gym {GymName = "Elixia"}
            };
            foreach (Gym g in gyms)
            {
                context.Gyms.Add(g);
            }

            context.SaveChanges();

            var employees = new Employee[]
            {
                new Employee{FirstName = "Layne", LastName = "Norton", Birthday = DateTime.Parse("1991-10-28"), Gender = Gender.Female, Country = "Norway", City = "Oslo", Email = "sathie@live.com", Certification = Certification.PersonalTrainer, Specialization = Specialization.SportsConditioning, GymId = 1},
                new Employee{FirstName = "Mark", LastName = "Bell",  Birthday = DateTime.Parse("1991-10-28"), Gender = Gender.Female, Country = "Norway", City = "Oslo", Email = "sathie@live.com", Certification = Certification.PersonalTrainer, Specialization = Specialization.SportsConditioning, GymId = 2},
                new Employee{FirstName = "Omar", LastName = "Isuf", Birthday = DateTime.Parse("1991-10-28"), Gender = Gender.Female, Country = "Norway", City = "Oslo", Email = "sathie@live.com", Certification = Certification.PersonalTrainer, Specialization = Specialization.SportsConditioning, GymId = 3}


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

            var workouts = new Workout[]
            {
                new Workout{WorkoutName = "Legs", WorkoutTypeId = 1},
                new Workout{WorkoutName = "Chest and Shoulders", WorkoutTypeId = 2}

            };
            foreach (Workout workout in workouts)
            {
                context.Workouts.Add(workout);
            }

            context.SaveChanges();

            //var exerciseAssigments = new ExerciseAssigment[]
            //{
            //    new ExerciseAssigment{WorkoutId = 1, ExerciseId = 1},
            //    new ExerciseAssigment{WorkoutId = 2, ExerciseId = 2}
            //};

            //foreach (ExerciseAssigment exerciseAssigment in exerciseAssigments)
            //{
            //    context.ExerciseAssigments.Add(exerciseAssigment);
            //}

            //context.SaveChanges();

            var workoutPrograms = new WorkoutProgram[]
            {
                new WorkoutProgram{WorkoutProgramName = "PHAT", WorkoutProgramDescription = "Layne Norton's Program"},
                new WorkoutProgram{WorkoutProgramName = "Starting Strength", WorkoutProgramDescription = "Mark Rippetoe's Program"}
            };

            foreach (WorkoutProgram workoutProgram in workoutPrograms)
            {
                context.WorkoutPrograms.Add(workoutProgram);
            }

            context.SaveChanges();

            //var workoutAssigments = new WorkoutAssigment[]
            //{
            //    new WorkoutAssigment{WorkoutId = 1, WorkoutProgramId = 1},
            //    new WorkoutAssigment{WorkoutId = 2, WorkoutProgramId = 2}
            //};

            //foreach (WorkoutAssigment workoutAssigment in workoutAssigments)
            //{
            //    context.WorkoutAssigments.Add(workoutAssigment);
            //}
            //context.SaveChanges();


        }
    }
}
