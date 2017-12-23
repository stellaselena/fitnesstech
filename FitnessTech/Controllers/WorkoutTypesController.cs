using FitnessTech.Data;
using FitnessTech.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FitnessTech.Repositories.Interfaces;
using FitnessTech.ViewModels;

namespace FitnessTech.Controllers
{
    public class WorkoutTypesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public IMapper _mapper { get; }

        public WorkoutTypesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: WorkoutTypes
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            var viewModel = new WorkoutTypeIndexData();

            viewModel.WorkoutTypes = await _unitOfWork.WorkoutTypeRepository.GetAllAsync();
            ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            switch (sortOrder)
            {
                case "name_desc":
                    viewModel.WorkoutTypes = viewModel.WorkoutTypes.OrderByDescending(c => c.WorkoutTypeName);
                    break;
                default:
                    viewModel.WorkoutTypes = viewModel.WorkoutTypes.OrderBy(c => c.WorkoutTypeName);
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.WorkoutTypes = viewModel.WorkoutTypes.Where(s => s.WorkoutTypeName.Contains(searchString));

            }
            return View(viewModel);
        }


        // GET: WorkoutTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutType = await _unitOfWork.WorkoutTypeRepository.GetBy(m => m.WorkoutTypeId == id);
            if (workoutType == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<WorkoutType, WorkoutTypeViewModel>(workoutType);
            return View(model);
        }

        // GET: WorkoutTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkoutTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkoutTypeId,WorkoutTypeName,WorkoutTypeDescription")] WorkoutTypeViewModel model)
        {
            var workoutType = _mapper.Map<WorkoutTypeViewModel, WorkoutType>(model);

            if (ModelState.IsValid)
            {
                await _unitOfWork.WorkoutTypeRepository.AddAsync(workoutType);
                await _unitOfWork.WorkoutTypeRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            var workoutTypeVm = _mapper.Map<WorkoutType, WorkoutTypeViewModel>(workoutType);

            return View(workoutTypeVm);
        }

        // GET: WorkoutTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutType = await _unitOfWork.WorkoutTypeRepository.GetBy(m => m.WorkoutTypeId == id);
            if (workoutType == null)
            {
                return NotFound();
            }
            var workoutTypeVm = _mapper.Map<WorkoutType, WorkoutTypeViewModel>(workoutType);

            return View(workoutTypeVm);
        }

        // POST: WorkoutTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkoutTypeId,WorkoutTypeName,WorkoutTypeDescription")] WorkoutTypeViewModel model)
        {
            var workoutType = _mapper.Map<WorkoutTypeViewModel, WorkoutType>(model);


            if (id != workoutType.WorkoutTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.WorkoutTypeRepository.Update(workoutType);
                    await _unitOfWork.WorkoutTypeRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.WorkoutTypeRepository.Exists(workoutType.WorkoutTypeId))
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
            var workoutTypeVm = _mapper.Map<WorkoutType, WorkoutTypeViewModel>(workoutType);

            return View(workoutTypeVm);
        }

        // GET: WorkoutTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutType = await _unitOfWork.WorkoutTypeRepository.GetBy(m => m.WorkoutTypeId == id);
            if (workoutType == null)
            {
                return NotFound();
            }
            var workoutTypeVm = _mapper.Map<WorkoutType, WorkoutTypeViewModel>(workoutType);

            return View(workoutTypeVm);
        }

        // POST: WorkoutTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workoutType = await _unitOfWork.WorkoutTypeRepository.GetBy(m => m.WorkoutTypeId == id);
            await _unitOfWork.WorkoutTypeRepository.DeleteAsync(workoutType);
            await _unitOfWork.WorkoutTypeRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
