using AutoMapper;
using FitnessTech.Data;
using FitnessTech.Data.Entities;
using FitnessTech.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly FitnessContext _context;
        private readonly IMapper _mapper;

        public ExercisesController(FitnessContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Exercises
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            var viewModel = new ExerciseIndexViewModel();
            viewModel.Exercises = await _context.Exercises.AsNoTracking().ToListAsync();
            ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["MuscleSort"] = String.IsNullOrEmpty(sortOrder) ? "muscle_desc" : "muscle";

            switch (sortOrder)
            {
                case "name_desc":
                    viewModel.Exercises = viewModel.Exercises.OrderByDescending(c => c.ExerciseName);
                    break;
                case "muscle_desc":
                    viewModel.Exercises = viewModel.Exercises.OrderByDescending(c => c.MuscleGroup);
                    break;
                case "muscle":
                    viewModel.Exercises = viewModel.Exercises.OrderBy(c => c.MuscleGroup);
                    break;
                default:
                    viewModel.Exercises = viewModel.Exercises.OrderBy(c => c.ExerciseName);
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.Exercises = viewModel.Exercises.Where(c => c.ExerciseName.Contains(searchString));
            }
            return View(viewModel);
        }

        // GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .SingleOrDefaultAsync(m => m.ExerciseId == id);
            if (exercise == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<Exercise, ExerciseViewModel>(exercise);
            return View(model);
        }

        // GET: Exercises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExerciseId,ExerciseName,ExerciseDescription,MuscleGroup")] ExerciseViewModel model)
        {
            var exercise = _mapper.Map<ExerciseViewModel, Exercise>(model);
            if (ModelState.IsValid)
            {
                _context.Add(exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var exerciseVm = _mapper.Map<Exercise, ExerciseViewModel>(exercise);

            return View(exerciseVm);
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises.SingleOrDefaultAsync(m => m.ExerciseId == id);
            if (exercise == null)
            {
                return NotFound();
            }
            var exerciseVm = _mapper.Map<Exercise, ExerciseViewModel>(exercise);

            return View(exerciseVm);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExerciseId,ExerciseName,ExerciseDescription,MuscleGroup")] ExerciseViewModel model)
        {
            var exercise = _mapper.Map<ExerciseViewModel, Exercise>(model);


            if (id != exercise.ExerciseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.ExerciseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var exerciseVm = _mapper.Map<Exercise, ExerciseViewModel>(exercise);

            return View(exerciseVm);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .SingleOrDefaultAsync(m => m.ExerciseId == id);
            if (exercise == null)
            {
                return NotFound();
            }
            var exerciseVm = _mapper.Map<Exercise, ExerciseViewModel>(exercise);

            return View(exerciseVm);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercise = await _context.Exercises.SingleOrDefaultAsync(m => m.ExerciseId == id);
            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.ExerciseId == id);
        }
    }
}
