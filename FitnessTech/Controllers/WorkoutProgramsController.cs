using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FitnessTech.Data;
using FitnessTech.Data.Entities;
using FitnessTech.Repositories.Interfaces;
using FitnessTech.ViewModels;

namespace FitnessTech.Controllers
{
    public class WorkoutProgramsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkoutProgramsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: WorkoutPrograms
        public async Task<IActionResult> Index(int? workoutId, string searchString, string sortOrder)
        {
            var viewModel = new WorkoutProgramIndexData();
            viewModel.WorkoutPrograms = await _unitOfWork.WorkoutProgramRepository.GetAllThenInclude();

            ViewData["WPNameSort"] = String.IsNullOrEmpty(sortOrder) ? "wpname_desc" : "";

            switch (sortOrder)
            {
                case "wpname_desc":
                    viewModel.WorkoutPrograms = viewModel.WorkoutPrograms.OrderByDescending(c => c.WorkoutProgramName);
                    break;
                default:
                    viewModel.WorkoutPrograms = viewModel.WorkoutPrograms.OrderBy(c => c.WorkoutProgramName);
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.WorkoutPrograms =
                    await _unitOfWork.WorkoutProgramRepository.FindAllAsync(w =>
                        w.WorkoutProgramName.Contains(searchString));

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

            var workoutProgram = await _unitOfWork.WorkoutProgramRepository.GetThenInclude(id);
            if (workoutProgram == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<WorkoutProgram, WorkoutProgramViewModel>(workoutProgram);
            return View(model);
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
            [Bind("WorkoutProgramId,WorkoutProgramName,WorkoutProgramDescription")] WorkoutProgramViewModel model,
            string[] selectedWorkouts)
        {
            var workoutProgram = _mapper.Map<WorkoutProgramViewModel, WorkoutProgram>(model);

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
                _unitOfWork.WorkoutProgramRepository.Add(workoutProgram);
                await _unitOfWork.WorkoutProgramRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAssignedWorkoutData(workoutProgram);
            var workoutProgramViewModel = _mapper.Map<WorkoutProgram, WorkoutProgramViewModel>(workoutProgram);

            return View(workoutProgramViewModel);
        }

        // GET: WorkoutPrograms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutProgram = await _unitOfWork.WorkoutProgramRepository.GetThenInclude(id);
            if (workoutProgram == null)
            {
                return NotFound();
            }
            PopulateAssignedWorkoutData(workoutProgram);
            var workoutProgramViewModel = _mapper.Map<WorkoutProgram, WorkoutProgramViewModel>(workoutProgram);

            return View(workoutProgramViewModel);
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
            var workoutProgram = await _unitOfWork.WorkoutProgramRepository.GetThenInclude(id);

            if (await TryUpdateModelAsync<WorkoutProgram>(
                workoutProgram,
                "",
                i => i.WorkoutProgramName, i => i.WorkoutProgramDescription))
            {
                UpdateWorkoutProgramWorkouts(selectedWorkouts, workoutProgram);
                try
                {
                    await _unitOfWork.WorkoutProgramRepository.SaveAsync();
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
            var workoutProgramViewModel = _mapper.Map<WorkoutProgram, WorkoutProgramViewModel>(workoutProgram);

            return View(workoutProgramViewModel);
        }

        // GET: WorkoutPrograms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutProgram = await _unitOfWork.WorkoutProgramRepository.GetBy(w => w.WorkoutProgramId == id);
            if (workoutProgram == null)
            {
                return NotFound();
            }
            var workoutProgramViewModel = _mapper.Map<WorkoutProgram, WorkoutProgramViewModel>(workoutProgram);

            return View(workoutProgramViewModel);
        }

        // POST: WorkoutPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workoutProgram = await _unitOfWork.WorkoutProgramRepository.GetBy(w => w.WorkoutProgramId == id);
            await _unitOfWork.WorkoutProgramRepository.DeleteAsync(workoutProgram);
            await _unitOfWork.WorkoutProgramRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateAssignedWorkoutData(WorkoutProgram workoutProgram)
        {
            var allWorkouts = _unitOfWork.WorkoutRepository.GetAll();
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
            var workouts = _unitOfWork.WorkoutRepository.GetAll();
            foreach (var workout in workouts)
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
                        WorkoutAssigment workoutToRemove =
                            workoutProgram.WorkoutAssigments.SingleOrDefault(i => i.WorkoutId == workout.WorkoutId);

                        workoutProgram.WorkoutAssigments.Remove(workoutToRemove);
                        _unitOfWork.WorkoutProgramRepository.SaveAsync();
                    }
                }
            }
        }
    }
}
