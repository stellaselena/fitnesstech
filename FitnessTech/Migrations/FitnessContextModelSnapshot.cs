﻿// <auto-generated />
using FitnessTech.Data;
using FitnessTech.Data.Entities;
using FitnessTech.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Nutritionix;
using System;

namespace FitnessTech.Migrations
{
    [DbContext(typeof(FitnessContext))]
    partial class FitnessContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FitnessTech.Data.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActivityLevel");

                    b.Property<byte[]>("AvatarImage");

                    b.Property<DateTime>("Birthday");

                    b.Property<int>("BodyFat");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Email");

                    b.Property<int?>("EmployeeId");

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

                    b.HasIndex("EmployeeId");

                    b.HasIndex("GymId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("FitnessTech.Data.Entities.CustomerStatistic", b =>
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

            modelBuilder.Entity("FitnessTech.Data.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("AvatarImage");

                    b.Property<DateTime>("Birthday");

                    b.Property<int>("Certification");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Email");

                    b.Property<int?>("EmployeeId");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<int>("Gender");

                    b.Property<int?>("GymId");

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<int>("Specialization");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("GymId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("FitnessTech.Data.Entities.Exercise", b =>
                {
                    b.Property<int>("ExerciseId")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("AvatarImage");

                    b.Property<string>("Equipment");

                    b.Property<string>("ExerciseDescription");

                    b.Property<string>("ExerciseName");

                    b.Property<int>("Level");

                    b.Property<int>("Mechanics");

                    b.Property<int>("MuscleGroup");

                    b.HasKey("ExerciseId");

                    b.ToTable("Exercise");
                });

            modelBuilder.Entity("FitnessTech.Data.Entities.ExerciseAssigment", b =>
                {
                    b.Property<int>("ExerciseId");

                    b.Property<int>("WorkoutId");

                    b.HasKey("ExerciseId", "WorkoutId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("ExerciseAssigment");
                });

            modelBuilder.Entity("FitnessTech.Data.Entities.Gym", b =>
                {
                    b.Property<int>("GymId")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("AvatarImage");

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

            modelBuilder.Entity("FitnessTech.Data.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("OrderDate");

                    b.Property<string>("OrderNumber");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("FitnessTech.Data.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("OrderId");

                    b.Property<int?>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("FitnessTech.Data.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<decimal>("Price");

                    b.Property<string>("Producer");

                    b.Property<string>("Size");

                    b.Property<string>("SupplementDescription");

                    b.Property<string>("SupplementId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("FitnessTech.Data.Entities.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RecipeDescription");

                    b.Property<string>("RecipeName")
                        .HasMaxLength(50);

                    b.HasKey("RecipeId");

                    b.ToTable("Recipe");
                });

            modelBuilder.Entity("FitnessTech.Data.Entities.Supplement", b =>
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

            modelBuilder.Entity("FitnessTech.Data.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("FitnessTech.Data.Entities.Workout", b =>
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

            modelBuilder.Entity("FitnessTech.Data.Entities.WorkoutAssigment", b =>
                {
                    b.Property<int>("WorkoutId");

                    b.Property<int>("WorkoutProgramId");

                    b.HasKey("WorkoutId", "WorkoutProgramId");

                    b.HasIndex("WorkoutProgramId");

                    b.ToTable("WorkoutAssigments");
                });

            modelBuilder.Entity("FitnessTech.Data.Entities.WorkoutProgram", b =>
                {
                    b.Property<int>("WorkoutProgramId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("WorkoutProgramDescription");

                    b.Property<string>("WorkoutProgramName")
                        .HasMaxLength(50);

                    b.HasKey("WorkoutProgramId");

                    b.ToTable("WorkoutProgram");
                });

            modelBuilder.Entity("FitnessTech.Data.Entities.WorkoutType", b =>
                {
                    b.Property<int>("WorkoutTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("WorkoutTypeDescription");

                    b.Property<string>("WorkoutTypeName")
                        .HasMaxLength(50);

                    b.HasKey("WorkoutTypeId");

                    b.ToTable("WorkoutType");
                });

            modelBuilder.Entity("FitnessTech.ViewModels.NutritionixItem", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BrandName");

                    b.Property<int>("ItemType");

                    b.Property<string>("Name");

                    b.Property<decimal?>("NutritionFact_Calories");

                    b.Property<decimal?>("NutritionFact_CaloriesFromFat");

                    b.Property<decimal?>("NutritionFact_DietaryFiber");

                    b.Property<decimal?>("NutritionFact_Protein");

                    b.Property<decimal?>("NutritionFact_ServingGramWeight");

                    b.Property<decimal?>("NutritionFact_Sugar");

                    b.Property<decimal?>("NutritionFact_TotalCarbohydrate");

                    b.Property<decimal?>("NutritionFact_TotalFat");

                    b.HasKey("Id");

                    b.ToTable("NutritionixItem");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FitnessTech.Data.Entities.Customer", b =>
                {
                    b.HasOne("FitnessTech.Data.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.HasOne("FitnessTech.Data.Entities.Gym", "Gym")
                        .WithMany("Customers")
                        .HasForeignKey("GymId");
                });

            modelBuilder.Entity("FitnessTech.Data.Entities.CustomerStatistic", b =>
                {
                    b.HasOne("FitnessTech.Data.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FitnessTech.Data.Entities.Gym", "Gym")
                        .WithMany()
                        .HasForeignKey("GymId");

                    b.HasOne("FitnessTech.Data.Entities.Employee", "ResponsibleEmployee")
                        .WithMany()
                        .HasForeignKey("ResponsibleEmployeeId");
                });

            modelBuilder.Entity("FitnessTech.Data.Entities.Employee", b =>
                {
                    b.HasOne("FitnessTech.Data.Entities.Employee")
                        .WithMany("Customers")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("FitnessTech.Data.Entities.Gym", "Gym")
                        .WithMany("Employees")
                        .HasForeignKey("GymId");
                });

            modelBuilder.Entity("FitnessTech.Data.Entities.ExerciseAssigment", b =>
                {
                    b.HasOne("FitnessTech.Data.Entities.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FitnessTech.Data.Entities.Workout", "Workout")
                        .WithMany("ExerciseAssigments")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FitnessTech.Data.Entities.Order", b =>
                {
                    b.HasOne("FitnessTech.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FitnessTech.Data.Entities.OrderItem", b =>
                {
                    b.HasOne("FitnessTech.Data.Entities.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId");

                    b.HasOne("FitnessTech.Data.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("FitnessTech.Data.Entities.Workout", b =>
                {
                    b.HasOne("FitnessTech.Data.Entities.WorkoutType", "WorkoutType")
                        .WithMany()
                        .HasForeignKey("WorkoutTypeId");
                });

            modelBuilder.Entity("FitnessTech.Data.Entities.WorkoutAssigment", b =>
                {
                    b.HasOne("FitnessTech.Data.Entities.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FitnessTech.Data.Entities.WorkoutProgram", "WorkoutProgram")
                        .WithMany("WorkoutAssigments")
                        .HasForeignKey("WorkoutProgramId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FitnessTech.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FitnessTech.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FitnessTech.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FitnessTech.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
