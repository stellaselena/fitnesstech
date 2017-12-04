using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FitnessTech.Data;
using FitnessTech.Models;

namespace FitnessTech.Controllers
{
    public class WorkoutsController : Controller
    {
        private readonly FitnessContext _context;

        public WorkoutsController(FitnessContext context)
        {
            _context = context;
        }

        // GET: Workouts
        public async Task<IActionResult> Index(int? id, int? exerciseId, string searchString)
        {
            var viewModel = new WorkoutIndexData();
            viewModel.Workouts = await _context.Workouts
                .Include(w => w.WorkoutType)
                .Include(w => w.ExerciseAssigments)
                .ThenInclude(w => w.Exercise)
                .OrderBy(w => w.WorkoutName)
                .ToListAsync();

            //if (id != null)
            //{
            //    ViewData["WorkoutId"] = id.Value;
            //    Workout workout = viewModel.Workouts.Where(
            //        i => i.WorkoutId == id.Value).Single();
            //    viewModel.Exercises = workout.ExerciseAssigments.Select(s => s.Exercise);
            //}

            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.Workouts = await _context.Workouts.Where(w => w.WorkoutName.Contains(searchString)).ToListAsync();
            }
            if (exerciseId != null)
            {
                ViewData["ExerciseId"] = exerciseId.Value;
            }

            return View(viewModel);
        }

        // GET: Workouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var workout = _context.Workouts.Include(w => w.WorkoutType)
                .Include(w => w.ExerciseAssigments).ThenInclude(w => w.Exercise).Single(w => w.WorkoutId == id);

            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // GET: Workouts/Create
        public IActionResult Create()
        {
            var workout = new Workout();
            workout.ExerciseAssigments = new List<ExerciseAssigment>();
            PopulateAssignedExerciseData(workout);
            ViewData["WorkoutTypeId"] = new SelectList(_context.WorkoutTypes, "WorkoutTypeId", "WorkoutTypeName");
            return View();
        }

        // POST: Workouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkoutId,WorkoutName,WorkoutTypeId")] Workout workout,
            string[] selectedExercises)
        {
            if (selectedExercises != null)
            {
                workout.ExerciseAssigments = new List<ExerciseAssigment>();
                foreach (var exercise in selectedExercises)
                {
                    var exerciseToAdd =
                        new ExerciseAssigment() {WorkoutId = workout.WorkoutId, ExerciseId = int.Parse(exercise)};
                    workout.ExerciseAssigments.Add(exerciseToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(workout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAssignedExerciseData(workout);
            ViewData["WorkoutTypeId"] =
                new SelectList(_context.WorkoutTypes, "WorkoutTypeId", "WorkoutTypeName", workout.WorkoutTypeId);
            return View(workout);
        }

        // GET: Workouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _context.Workouts
                .Include(i => i.WorkoutType)
                .Include(i => i.ExerciseAssigments).ThenInclude(i => i.Exercise)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.WorkoutId == id);
            if (workout == null)
            {
                return NotFound();
            }
            PopulateAssignedExerciseData(workout);
            ViewData["WorkoutTypeId"] =
                new SelectList(_context.WorkoutTypes, "WorkoutTypeId", "WorkoutTypeName");
            return View(workout);
        }

        // POST: Workouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedExercises)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _context.Workouts
                .Include(i => i.WorkoutType)
                .Include(i => i.ExerciseAssigments)
                .ThenInclude(i => i.Exercise)
                .SingleOrDefaultAsync(m => m.WorkoutId == id);

            if (await TryUpdateModelAsync<Workout>(
                workout,
                "",
                i => i.WorkoutName, i => i.WorkoutType))
            {
                UpdateWorkoutExercises(selectedExercises, workout);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                                                 "Try again, and if the problem persists, " +
                                                 "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateWorkoutExercises(selectedExercises, workout);
            PopulateAssignedExerciseData(workout);
            
            return View(workout);
        }

        // GET: Workouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _context.Workouts.Include(w => w.WorkoutType)
                .SingleOrDefaultAsync(m => m.WorkoutId == id);
            if (workout == null)
            {
                return NotFound();
            }
            return View(workout);
        }

        // POST: Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workout = await _context.Workouts.SingleOrDefaultAsync(m => m.WorkoutId == id);
            _context.Workouts.Remove(workout);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutExists(int id)
        {
            return _context.Workouts.Any(e => e.WorkoutId == id);
        }

        private void PopulateAssignedExerciseData(Workout workout)
        {
            var allExercises = _context.Exercises;
            var workoutExercises = new HashSet<int>(workout.ExerciseAssigments.Select(e => e.ExerciseId));
            var viewModel = new List<AssignedExerciseData>();
            foreach (var exercise in allExercises)
            {
                viewModel.Add(new AssignedExerciseData()
                {
                    ExerciseId = exercise.ExerciseId,
                    ExerciseName = exercise.ExerciseName,
                    Assigned = workoutExercises.Contains(exercise.ExerciseId)
                });
            }
            ViewData["Exercises"] = viewModel;
        }

        private void UpdateWorkoutExercises(string[] selectedExercises, Workout workout)
        {
            if (selectedExercises == null)
            {
                workout.ExerciseAssigments = new List<ExerciseAssigment>();
                return;
            }

            var selectedExercisesHS = new HashSet<string>(selectedExercises);
            var workoutExercises = new HashSet<int>
                (workout.ExerciseAssigments.Select(c => c.Exercise.ExerciseId));
            foreach (var exercise in _context.Exercises)
            {
                if (selectedExercisesHS.Contains(exercise.ExerciseId.ToString()))
                {
                    if (!workoutExercises.Contains(exercise.ExerciseId))
                    {
                        workout.ExerciseAssigments.Add(new ExerciseAssigment()
                        {
                            WorkoutId = workout.WorkoutId,
                            ExerciseId = exercise.ExerciseId
                        });
                    }
                }
                else
                {
                    if (workoutExercises.Contains(exercise.ExerciseId))
                    {
                        ExerciseAssigment courseToRemove =
                            workout.ExerciseAssigments.SingleOrDefault(i => i.ExerciseId == exercise.ExerciseId);
                        _context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
