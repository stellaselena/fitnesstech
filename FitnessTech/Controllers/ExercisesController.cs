using AutoMapper;
using FitnessTech.Data.Entities;
using FitnessTech.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Data.Helpers;
using FitnessTech.Repositories.Interfaces;


namespace FitnessTech.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        

        public ExercisesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: Exercises
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            var viewModel = new ExerciseIndexViewModel();
            viewModel.Exercises = await _unitOfWork.ExerciseRepository.GetAllAsync();
            ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["MuscleSort"] = String.IsNullOrEmpty(sortOrder) ? "muscle_desc" : "";

            switch (sortOrder)
            {
                case "name_desc":
                    viewModel.Exercises = viewModel.Exercises.OrderByDescending(c => c.ExerciseName);
                    break;
                case "muscle_desc":
                    viewModel.Exercises = viewModel.Exercises.OrderByDescending(c => c.MuscleGroup);
                    break;
                default:
                    viewModel.Exercises = viewModel.Exercises.OrderBy(c => c.ExerciseName);
                    break;
            }
           

            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.Exercises =
                    await _unitOfWork.ExerciseRepository.FindAllAsync(w =>
                        w.ExerciseName.Contains(searchString));

            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(ExerciseIndexViewModel viewModel)
        {
            List<Exercise> selectedMuscleGroups = new List<Exercise>();
            if (viewModel.Legs)
            {
                var result = _unitOfWork.ExerciseRepository.GetByMuscleGroup(MuscleGroup.Legs);
                foreach (var res in result)
                {
                    selectedMuscleGroups.Add(res);

                }
            }
            if (viewModel.Back)
            {
                var result = _unitOfWork.ExerciseRepository.GetByMuscleGroup(MuscleGroup.Back);
                foreach (var res in result)
                {
                    selectedMuscleGroups.Add(res);

                }
            }
            if (viewModel.Chest)
            {
                var result = _unitOfWork.ExerciseRepository.GetByMuscleGroup(MuscleGroup.Chest);
                foreach (var res in result)
                {
                    selectedMuscleGroups.Add(res);

                }
            }
            if (viewModel.Shoulders)
            {
                var result = _unitOfWork.ExerciseRepository.GetByMuscleGroup(MuscleGroup.Shoulders);
                foreach (var res in result)
                {
                    selectedMuscleGroups.Add(res);

                }
            }
            if (viewModel.Arms)
            {
                var result = _unitOfWork.ExerciseRepository.GetByMuscleGroup(MuscleGroup.Arms);
                foreach (var res in result)
                {
                    selectedMuscleGroups.Add(res);

                }
            }
            if (viewModel.Abdominals)
            {
                var result = _unitOfWork.ExerciseRepository.GetByMuscleGroup(MuscleGroup.Abdominals);
                foreach (var res in result)
                {
                    selectedMuscleGroups.Add(res);

                }
            }

            viewModel.Exercises = selectedMuscleGroups;


            return View(viewModel);
        }

        public void ResetFilter()
        {
           Index(null, null);

        }

        // GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _unitOfWork.ExerciseRepository.GetBy(e => e.ExerciseId == id);
            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
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
        public async Task<IActionResult> Create([Bind("ExerciseId,ExerciseName,ExerciseDescription,MuscleGroup,AvatarImage")] ExerciseViewModel model)
        {
            var exercise = _mapper.Map<ExerciseViewModel, Exercise>(model);

            if (ModelState.IsValid)
            {
                
                using (var memoryStream = new MemoryStream())
                {
                    await model.AvatarImage.CopyToAsync(memoryStream);
                    exercise.AvatarImage = memoryStream.ToArray();

                }
                await _unitOfWork.ExerciseRepository.AddAsync(exercise);
                await _unitOfWork.ExerciseRepository.SaveAsync();
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

            var exercise = await _unitOfWork.ExerciseRepository.GetBy(m => m.ExerciseId == id);
            if (exercise == null)
            {
                return NotFound();
            }

            var exerciseVm = new ExerciseViewModel();
           
            exerciseVm.ExerciseId = exercise.ExerciseId;
            exerciseVm.ExerciseName = exercise.ExerciseName;
            exerciseVm.ExerciseDescription = exercise.ExerciseDescription;
            exerciseVm.MuscleGroup = exercise.MuscleGroup;

            return View(exerciseVm);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExerciseId,ExerciseName,ExerciseDescription,MuscleGroup,AvatarImage")] ExerciseViewModel model)
        {
            //var exercise = _mapper.Map<ExerciseViewModel, Exercise>(model);
            var exercise = _unitOfWork.ExerciseRepository.Get(model.ExerciseId);
            

            if (id != exercise.ExerciseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                exercise.ExerciseName = model.ExerciseName;
                exercise.ExerciseDescription = model.ExerciseDescription;
                exercise.MuscleGroup = model.MuscleGroup;
                if (model.AvatarImage != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.AvatarImage.CopyToAsync(memoryStream);
                        exercise.AvatarImage = memoryStream.ToArray();

                    }
                }
               
                try
                {
                    _unitOfWork.ExerciseRepository.Update(exercise);
                    await _unitOfWork.ExerciseRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.ExerciseRepository.Exists(exercise.ExerciseId))
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

            var exercise = await _unitOfWork.ExerciseRepository.GetBy(m => m.ExerciseId == id);
            if (exercise == null)
            {
                return NotFound();
            }
            var exerciseVm = new ExerciseViewModel();
            exerciseVm.ExerciseId = exercise.ExerciseId;
            exerciseVm.ExerciseName = exercise.ExerciseName;
            exerciseVm.ExerciseDescription = exercise.ExerciseDescription;
            exerciseVm.MuscleGroup = exercise.MuscleGroup;

            return View(exerciseVm);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercise = await _unitOfWork.ExerciseRepository.GetBy(m => m.ExerciseId == id);
            await _unitOfWork.ExerciseRepository.DeleteAsync(exercise);
            await _unitOfWork.ExerciseRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

       


    }
}
