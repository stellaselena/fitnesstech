using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessTech.Data
{
    public class FitnessContext : DbContext
    {

        public FitnessContext(DbContextOptions<FitnessContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerStatistic> CustomerSet { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Supplement> Supplements { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutProgram> WorkoutPrograms { get; set; }
        public DbSet<WorkoutType> WorkoutTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<CustomerStatistic>().ToTable("CustomerStatistic");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Exercise>().ToTable("Exercise");
            modelBuilder.Entity<Gym>().ToTable("Gym");
            modelBuilder.Entity<Recipe>().ToTable("Recipe");
            modelBuilder.Entity<Supplement>().ToTable("Supplement");
            modelBuilder.Entity<Workout>().ToTable("Workout");
            modelBuilder.Entity<WorkoutProgram>().ToTable("WorkoutProgram");
            modelBuilder.Entity<WorkoutType>().ToTable("WorkoutType");
        }
    }
}
