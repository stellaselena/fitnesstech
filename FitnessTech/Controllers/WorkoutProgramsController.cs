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
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkoutPrograms.ToListAsync());
        }

        // GET: WorkoutPrograms/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: WorkoutPrograms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkoutPrograms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkoutProgramId,WorkoutProgramName,WorkoutProgramDescription")] WorkoutProgram workoutProgram)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workoutProgram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workoutProgram);
        }

        // GET: WorkoutPrograms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutProgram = await _context.WorkoutPrograms.SingleOrDefaultAsync(m => m.WorkoutProgramId == id);
            if (workoutProgram == null)
            {
                return NotFound();
            }
            return View(workoutProgram);
        }

        // POST: WorkoutPrograms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkoutProgramId,WorkoutProgramName,WorkoutProgramDescription")] WorkoutProgram workoutProgram)
        {
            if (id != workoutProgram.WorkoutProgramId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workoutProgram);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutProgramExists(workoutProgram.WorkoutProgramId))
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
    }
}
