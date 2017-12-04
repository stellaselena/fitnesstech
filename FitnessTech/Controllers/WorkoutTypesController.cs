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
    public class WorkoutTypesController : Controller
    {
        private readonly FitnessContext _context;

        public WorkoutTypesController(FitnessContext context)
        {
            _context = context;
        }

        // GET: WorkoutTypes
        public async Task<IActionResult> Index(string searchString)
        {
            var workoutTypes = from wt in _context.WorkoutTypes
                select wt;
            if (!String.IsNullOrEmpty(searchString))
            {
                workoutTypes = workoutTypes.Where(s => s.WorkoutTypeName.Contains(searchString));

            }
            return View(await workoutTypes.ToListAsync());
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

            return View(workoutType);
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
        public async Task<IActionResult> Create([Bind("WorkoutTypeId,WorkoutTypeName,WorkoutTypeDescription")] WorkoutType workoutType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workoutType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workoutType);
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
            return View(workoutType);
        }

        // POST: WorkoutTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkoutTypeId,WorkoutTypeName,WorkoutTypeDescription")] WorkoutType workoutType)
        {
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
            return View(workoutType);
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

            return View(workoutType);
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
