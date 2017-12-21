using AutoMapper;
using FitnessTech.Data;
using FitnessTech.Data.Entities;
using FitnessTech.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Data.Helpers;

namespace FitnessTech.Controllers
{
    public class GymsController : Controller
    {
        private readonly FitnessContext _context;
        private readonly IMapper _mapper;

        public GymsController(FitnessContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Gyms
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            var viewModel = new GymIndexViewModel();
            viewModel.Gyms = await _context.Gyms.AsNoTracking().ToListAsync();
            ViewData["GymNameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CountrySort"] = String.IsNullOrEmpty(sortOrder) ? "country_desc" : "country";

            switch (sortOrder)
            {
                case "name_desc":
                    viewModel.Gyms = viewModel.Gyms.OrderByDescending(c => c.GymName);
                    break;
                case "country_desc":
                    viewModel.Gyms = viewModel.Gyms.OrderByDescending(c => c.Country);
                    break;
                case "country":
                    viewModel.Gyms = viewModel.Gyms.OrderBy(c => c.Country);
                    break;
                default:
                    viewModel.Gyms = viewModel.Gyms.OrderBy(c => c.GymName);
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.Gyms = viewModel.Gyms.Where(g => g.GymName.Contains(searchString));

            }

            return View(viewModel);

        }

        // GET: Gyms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gym = await _context.Gyms.Include(g => g.Employees).Include(g => g.Customers)
                .SingleOrDefaultAsync(m => m.GymId == id);
            if (gym == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<Gym, GymViewModel>(gym);
            return View(model);
        }

        // GET: Gyms/Create
        public IActionResult Create()
        {
            ViewBag.Countries = new SelectList(Country.GetCountries(), "ID", "Name");

            return View();
        }

        // POST: Gyms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GymId,GymName,Description,Website,Email,Country,City,PostalCode,Street,PhoneNumber")] GymViewModel model)
        {
            var gym = _mapper.Map<GymViewModel, Gym>(model);
            if (ModelState.IsValid)
            {
                _context.Add(gym);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var gymVm = _mapper.Map<Gym, GymViewModel>(gym);

            return View(gymVm);
        }

        // GET: Gyms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gym = await _context.Gyms.SingleOrDefaultAsync(m => m.GymId == id);
            if (gym == null)
            {
                return NotFound();
            }
            ViewBag.Countries = new SelectList(Country.GetCountries(), "ID", "Name");
            var gymVm = _mapper.Map<Gym, GymViewModel>(gym);

            return View(gymVm);
        }

        // POST: Gyms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GymId,GymName,Description,Website,Email,Country,City,PostalCode,Street,PhoneNumber")] GymViewModel model)
        {
            var gym = _mapper.Map<GymViewModel, Gym>(model);

            if (id != gym.GymId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gym);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymExists(gym.GymId))
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
            ViewBag.Countries = new SelectList(Country.GetCountries(), "ID", "Name", gym.Country);
            var gymVm = _mapper.Map<Gym, GymViewModel>(gym);

            return View(gymVm);
        }

        // GET: Gyms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gym = await _context.Gyms
                .SingleOrDefaultAsync(m => m.GymId == id);
            if (gym == null)
            {
                return NotFound();
            }
            var gymVm = _mapper.Map<Gym, GymViewModel>(gym);

            return View(gymVm);
        }

        // POST: Gyms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gym = await _context.Gyms.SingleOrDefaultAsync(m => m.GymId == id);
            _context.Gyms.Remove(gym);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GymExists(int id)
        {
            return _context.Gyms.Any(e => e.GymId == id);
        }

    }
}
