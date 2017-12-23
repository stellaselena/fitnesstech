using AutoMapper;
using FitnessTech.Data;
using FitnessTech.Data.Entities;
using FitnessTech.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Repositories.Interfaces;

namespace FitnessTech.Controllers
{
    public class WorkoutsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public IMapper _mapper { get; }

        public WorkoutsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: Workouts
        public async Task<IActionResult> Index(int? id, int? exerciseId, string searchString, string sortOrder)
        {
            var viewModel = new WorkoutIndexData();
            viewModel.Workouts = await _unitOfWork.WorkoutRepository.GetAllThenInclude();
            ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            switch (sortOrder)
            {
                case "name_desc":
                    viewModel.Workouts = viewModel.Workouts.OrderByDescending(c => c.WorkoutName);
                    break;
                default:
                    viewModel.Workouts = viewModel.Workouts.OrderBy(c => c.WorkoutName);
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.Workouts =
                    await _unitOfWork.WorkoutRepository.FindAllAsync(w => w.WorkoutName.Contains(searchString));
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


            var workout = await _unitOfWork.WorkoutRepository.GetThenInclude(id);

            if (workout == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<Workout, WorkoutViewModel>(workout);
            return View(model);
        }

        // GET: Workouts/Create
        public IActionResult Create()
        {
            var workout = new Workout();
            workout.ExerciseAssigments = new List<ExerciseAssigment>();
            PopulateAssignedExerciseData(workout);
            ViewData["WorkoutTypeId"] = new SelectList(_unitOfWork.WorkoutTypeRepository.GetAll(), "WorkoutTypeId", "WorkoutTypeName");
            return View();
        }

        // POST: Workouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkoutId,WorkoutName,WorkoutTypeId")] WorkoutViewModel model,
            string[] selectedExercises)
        {
            var workout = _mapper.Map<WorkoutViewModel, Workout>(model);
            if (selectedExercises != null)
            {
                workout.ExerciseAssigments = new List<ExerciseAssigment>();
                foreach (var exercise in selectedExercises)
                {
                    var exerciseToAdd =
                        new ExerciseAssigment() { WorkoutId = workout.WorkoutId, ExerciseId = int.Parse(exercise) };
                    workout.ExerciseAssigments.Add(exerciseToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.WorkoutRepository.Add(workout);
                await _unitOfWork.WorkoutRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAssignedExerciseData(workout);
            ViewData["WorkoutTypeId"] =
                new SelectList(_unitOfWork.WorkoutTypeRepository.GetAll(), "WorkoutTypeId", "WorkoutTypeName", workout.WorkoutTypeId);
            var workoutVm = _mapper.Map<Workout, WorkoutViewModel>(workout);

            return View(workoutVm);
        }

        // GET: Workouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _unitOfWork.WorkoutRepository.GetThenInclude(id);
               
            if (workout == null)
            {
                return NotFound();
            }
            PopulateAssignedExerciseData(workout);
            ViewData["WorkoutTypeId"] = new SelectList(_unitOfWork.WorkoutTypeRepository.GetAll(), "WorkoutTypeId", "WorkoutTypeName");
            var workoutVm = _mapper.Map<Workout, WorkoutViewModel>(workout);

            return View(workoutVm);
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

            var workout = await _unitOfWork.WorkoutRepository.GetThenInclude(id);
               
            if (await TryUpdateModelAsync<Workout>(
                workout,
                "",
                i => i.WorkoutName, i => i.WorkoutTypeId))
            {
                UpdateWorkoutExercises(selectedExercises, workout);
                try
                {
                    await _unitOfWork.WorkoutRepository.SaveAsync();
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
            var workoutVm = _mapper.Map<Workout, WorkoutViewModel>(workout);

            return View(workoutVm);
        }

        // GET: Workouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _unitOfWork.WorkoutRepository.GetBy(e => e.WorkoutId == id);
            if (workout == null)
            {
                return NotFound();
            }
            var workoutVm = _mapper.Map<Workout, WorkoutViewModel>(workout);

            return View(workoutVm);
        }

        // POST: Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workout = await _unitOfWork.WorkoutRepository.GetBy(w => w.WorkoutId == id);
            await _unitOfWork.WorkoutRepository.DeleteAsync(workout);

            await _unitOfWork.WorkoutRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateAssignedExerciseData(Workout workout)
        {
            var allExercises = _unitOfWork.ExerciseRepository.GetAll();
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
            var exercises = _unitOfWork.ExerciseRepository.GetAll();
            foreach (var exercise in exercises)
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
                        ExerciseAssigment exerciseToRemove =
                            workout.ExerciseAssigments.SingleOrDefault(i => i.ExerciseId == exercise.ExerciseId);

                         workout.ExerciseAssigments.Remove(exerciseToRemove);
                         _unitOfWork.WorkoutRepository.SaveAsync();

                       
                    }
                }
            }
        }
    }
}
