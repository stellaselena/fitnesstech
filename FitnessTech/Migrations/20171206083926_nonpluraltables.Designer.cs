﻿// <auto-generated />
using FitnessTech.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace FitnessTech.Migrations
{
    [DbContext(typeof(FitnessContext))]
    [Migration("20171206083926_nonpluraltables")]
    partial class nonpluraltables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FitnessTech.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActivityLevel");

                    b.Property<DateTime>("Birthday");

                    b.Property<int>("BodyFat");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<int>("Gender");

                    b.Property<int>("Goal");

                    b.Property<int?>("GymId");

                    b.Property<int>("Height");

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<int>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("GymId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("FitnessTech.Models.CustomerStatistic", b =>
                {
                    b.Property<int>("CustomerStatisticsId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<int?>("GymId");

                    b.Property<int?>("ResponsibleEmployeeId");

                    b.HasKey("CustomerStatisticsId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("GymId");

                    b.HasIndex("ResponsibleEmployeeId");

                    b.ToTable("CustomerStatistic");
                });

            modelBuilder.Entity("FitnessTech.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Birthday");

                    b.Property<int>("Certification");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<int>("Gender");

                    b.Property<int?>("GymId");

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<int>("Specialization");

                    b.HasKey("Id");

                    b.HasIndex("GymId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("FitnessTech.Models.Exercise", b =>
                {
                    b.Property<int>("ExerciseId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ExerciseDescription");

                    b.Property<string>("ExerciseName");

                    b.Property<int>("MuscleGroup");

                    b.HasKey("ExerciseId");

                    b.ToTable("Exercise");
                });

            modelBuilder.Entity("FitnessTech.Models.ExerciseAssigment", b =>
                {
                    b.Property<int>("ExerciseId");

                    b.Property<int>("WorkoutId");

                    b.HasKey("ExerciseId", "WorkoutId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("ExerciseAssigment");
                });

            modelBuilder.Entity("FitnessTech.Models.Gym", b =>
                {
                    b.Property<int>("GymId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<string>("GymName")
                        .HasMaxLength(50);

                    b.Property<int>("PhoneNumber");

                    b.Property<int>("PostalCode");

                    b.Property<string>("Street");

                    b.Property<string>("Website");

                    b.HasKey("GymId");

                    b.ToTable("Gym");
                });

            modelBuilder.Entity("FitnessTech.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RecipeDescription");

                    b.Property<string>("RecipeName")
                        .HasMaxLength(50);

                    b.HasKey("RecipeId");

                    b.ToTable("Recipe");
                });

            modelBuilder.Entity("FitnessTech.Models.Supplement", b =>
                {
                    b.Property<int>("SupplementId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SupplementDescription");

                    b.Property<int>("SupplementName")
                        .HasMaxLength(50);

                    b.Property<int>("SupplementType");

                    b.HasKey("SupplementId");

                    b.ToTable("Supplement");
                });

            modelBuilder.Entity("FitnessTech.Models.Workout", b =>
                {
                    b.Property<int>("WorkoutId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("WorkoutName")
                        .HasMaxLength(50);

                    b.Property<int?>("WorkoutTypeId");

                    b.HasKey("WorkoutId");

                    b.HasIndex("WorkoutTypeId");

                    b.ToTable("Workout");
                });

            modelBuilder.Entity("FitnessTech.Models.WorkoutAssigment", b =>
                {
                    b.Property<int>("WorkoutId");

                    b.Property<int>("WorkoutProgramId");

                    b.HasKey("WorkoutId", "WorkoutProgramId");

                    b.HasIndex("WorkoutProgramId");

                    b.ToTable("WorkoutAssigments");
                });

            modelBuilder.Entity("FitnessTech.Models.WorkoutProgram", b =>
                {
                    b.Property<int>("WorkoutProgramId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("WorkoutProgramDescription");

                    b.Property<string>("WorkoutProgramName")
                        .HasMaxLength(50);

                    b.HasKey("WorkoutProgramId");

                    b.ToTable("WorkoutProgram");
                });

            modelBuilder.Entity("FitnessTech.Models.WorkoutType", b =>
                {
                    b.Property<int>("WorkoutTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("WorkoutTypeDescription");

                    b.Property<string>("WorkoutTypeName")
                        .HasMaxLength(50);

                    b.HasKey("WorkoutTypeId");

                    b.ToTable("WorkoutType");
                });

            modelBuilder.Entity("FitnessTech.Models.Customer", b =>
                {
                    b.HasOne("FitnessTech.Models.Gym")
                        .WithMany("Customers")
                        .HasForeignKey("GymId");
                });

            modelBuilder.Entity("FitnessTech.Models.CustomerStatistic", b =>
                {
                    b.HasOne("FitnessTech.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FitnessTech.Models.Gym", "Gym")
                        .WithMany()
                        .HasForeignKey("GymId");

                    b.HasOne("FitnessTech.Models.Employee", "ResponsibleEmployee")
                        .WithMany()
                        .HasForeignKey("ResponsibleEmployeeId");
                });

            modelBuilder.Entity("FitnessTech.Models.Employee", b =>
                {
                    b.HasOne("FitnessTech.Models.Gym", "Gym")
                        .WithMany("Employees")
                        .HasForeignKey("GymId");
                });

            modelBuilder.Entity("FitnessTech.Models.ExerciseAssigment", b =>
                {
                    b.HasOne("FitnessTech.Models.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FitnessTech.Models.Workout", "Workout")
                        .WithMany("ExerciseAssigments")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FitnessTech.Models.Workout", b =>
                {
                    b.HasOne("FitnessTech.Models.WorkoutType", "WorkoutType")
                        .WithMany()
                        .HasForeignKey("WorkoutTypeId");
                });

            modelBuilder.Entity("FitnessTech.Models.WorkoutAssigment", b =>
                {
                    b.HasOne("FitnessTech.Models.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FitnessTech.Models.WorkoutProgram", "WorkoutProgram")
                        .WithMany("WorkoutAssigments")
                        .HasForeignKey("WorkoutProgramId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
