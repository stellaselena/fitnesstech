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
    public class WorkoutProgramsController : Controller
    {
        private readonly FitnessContext _context;

        public WorkoutProgramsController(FitnessContext context)
        {
            _context = context;
        }

        // GET: WorkoutPrograms
        public async Task<IActionResult> Index(int? workoutId, string searchString)
        {
            var viewModel = new WorkoutProgramIndexData();
            viewModel.WorkoutPrograms = await _context.WorkoutPrograms
                .Include(wp => wp.WorkoutAssigments)
                .ThenInclude(wp => wp.Workout)
                .OrderBy(w => w.WorkoutProgramName)
                .ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.WorkoutPrograms = await _context.WorkoutPrograms
                    .Where(w => w.WorkoutProgramName.Contains(searchString)).ToListAsync();
            }
            if (workoutId != null)
            {
                ViewData["WorkoutId"] = workoutId.Value;
            }
            return View(viewModel);
        }

        // GET: WorkoutPrograms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutProgram = await _context.WorkoutPrograms.Include(w => w.WorkoutAssigments)
                .ThenInclude(w => w.Workout).ThenInclude(w => w.WorkoutType)
                .Include(w => w.WorkoutAssigments).ThenInclude(w => w.Workout)
                .ThenInclude(w => w.ExerciseAssigments)
                .ThenInclude(w => w.Exercise)
                .SingleOrDefaultAsync(m => m.WorkoutProgramId == id);
            if (workoutProgram == null)
            {
                return NotFound();
            }

            return View(workoutProgram);
        }

        // GET: WorkoutPrograms/Create
        public IActionResult Create()
        {
            var workoutProgram = new WorkoutProgram();
            workoutProgram.WorkoutAssigments = new List<WorkoutAssigment>();
            PopulateAssignedWorkoutData(workoutProgram);
            return View();
        }

        // POST: WorkoutPrograms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("WorkoutProgramId,WorkoutProgramName,WorkoutProgramDescription")] WorkoutProgram workoutProgram,
            string[] selectedWorkouts)
        {
            if (selectedWorkouts != null)
            {
                workoutProgram.WorkoutAssigments = new List<WorkoutAssigment>();
                foreach (var workout in selectedWorkouts)
                {
                    var workoutsToAdd =
                        new WorkoutAssigment()
                        {
                            WorkoutProgramId = workoutProgram.WorkoutProgramId,
                            WorkoutId = int.Parse(workout)
                        };
                    workoutProgram.WorkoutAssigments.Add(workoutsToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(workoutProgram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAssignedWorkoutData(workoutProgram);

            return View(workoutProgram);
        }

        // GET: WorkoutPrograms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutProgram = await _context.WorkoutPrograms
                .Include(w => w.WorkoutAssigments).ThenInclude(w => w.Workout).AsNoTracking()
                .SingleOrDefaultAsync(m => m.WorkoutProgramId == id);
            if (workoutProgram == null)
            {
                return NotFound();
            }
            PopulateAssignedWorkoutData(workoutProgram);
            return View(workoutProgram);
        }

        // POST: WorkoutPrograms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedWorkouts)
        {
            if (id == null)
            {
                return NotFound();
            }
            var workoutProgram = await _context.WorkoutPrograms
                .Include(i => i.WorkoutAssigments)
                .ThenInclude(i => i.Workout)
                .SingleOrDefaultAsync(m => m.WorkoutProgramId == id);

            if (await TryUpdateModelAsync<WorkoutProgram>(
                workoutProgram,
                "",
                i => i.WorkoutProgramName, i => i.WorkoutProgramDescription))
            {
                UpdateWorkoutProgramWorkouts(selectedWorkouts, workoutProgram);
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


            UpdateWorkoutProgramWorkouts(selectedWorkouts, workoutProgram);
            PopulateAssignedWorkoutData(workoutProgram);

            return View(workoutProgram);
        }

        // GET: WorkoutPrograms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutProgram = await _context.WorkoutPrograms
                .SingleOrDefaultAsync(m => m.WorkoutProgramId == id);
            if (workoutProgram == null)
            {
                return NotFound();
            }

            return View(workoutProgram);
        }

        // POST: WorkoutPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workoutProgram = await _context.WorkoutPrograms.SingleOrDefaultAsync(m => m.WorkoutProgramId == id);
            _context.WorkoutPrograms.Remove(workoutProgram);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutProgramExists(int id)
        {
            return _context.WorkoutPrograms.Any(e => e.WorkoutProgramId == id);
        }

        private void PopulateAssignedWorkoutData(WorkoutProgram workoutProgram)
        {
            var allWorkouts = _context.Workouts;
            var workoutExercises = new HashSet<int>(workoutProgram.WorkoutAssigments.Select(e => e.WorkoutId));
            var viewModel = new List<AssignedWorkoutData>();
            foreach (var workout in allWorkouts)
            {
                viewModel.Add(new AssignedWorkoutData()
                {
                    WorkoutId = workout.WorkoutId,
                    WorkoutName = workout.WorkoutName,
                    Assigned = workoutExercises.Contains(workout.WorkoutId)
                });
            }
            ViewData["Workouts"] = viewModel;
        }

        private void UpdateWorkoutProgramWorkouts(string[] selectedWorkouts, WorkoutProgram workoutProgram)
        {
            if (selectedWorkouts == null)
            {
                workoutProgram.WorkoutAssigments = new List<WorkoutAssigment>();
                return;
            }

            var selectedWorkoutsHS = new HashSet<string>(selectedWorkouts);
            var workoutExercises = new HashSet<int>
                (workoutProgram.WorkoutAssigments.Select(c => c.Workout.WorkoutId));
            foreach (var workout in _context.Workouts)
            {
                if (selectedWorkoutsHS.Contains(workout.WorkoutId.ToString()))
                {
                    if (!workoutExercises.Contains(workout.WorkoutId))
                    {
                        workoutProgram.WorkoutAssigments.Add(new WorkoutAssigment()
                        {
                            WorkoutProgramId = workoutProgram.WorkoutProgramId,
                            WorkoutId = workout.WorkoutId
                        });
                    }
                }
                else
                {
                    if (workoutExercises.Contains(workout.WorkoutId))
                    {
                        WorkoutAssigment courseToRemove =
                            workoutProgram.WorkoutAssigments.SingleOrDefault(i => i.WorkoutId == workout.WorkoutId);
                        _context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
