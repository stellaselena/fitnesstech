using FitnessTech.Data;
using FitnessTech.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FitnessTech.ViewModels;

namespace FitnessTech.Controllers
{
    public class WorkoutTypesController : Controller
    {
        private readonly FitnessContext _context;

        public IMapper _mapper { get; }

        public WorkoutTypesController(FitnessContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: WorkoutTypes
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            var viewModel = new WorkoutTypeIndexData();

            viewModel.WorkoutTypes = await _context.WorkoutTypes.AsNoTracking().ToListAsync();
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

            var workoutType = await _context.WorkoutTypes
                .SingleOrDefaultAsync(m => m.WorkoutTypeId == id);
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
                _context.Add(workoutType);
                await _context.SaveChangesAsync();
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

            var workoutType = await _context.WorkoutTypes.SingleOrDefaultAsync(m => m.WorkoutTypeId == id);
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
                    _context.Update(workoutType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutTypeExists(workoutType.WorkoutTypeId))
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

            var workoutType = await _context.WorkoutTypes
                .SingleOrDefaultAsync(m => m.WorkoutTypeId == id);
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
            var workoutType = await _context.WorkoutTypes.SingleOrDefaultAsync(m => m.WorkoutTypeId == id);
            _context.WorkoutTypes.Remove(workoutType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutTypeExists(int id)
        {
            return _context.WorkoutTypes.Any(e => e.WorkoutTypeId == id);
        }
    }
}
