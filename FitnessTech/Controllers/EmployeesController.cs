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
    public class EmployeesController : Controller
    {
        private readonly FitnessContext _context;
        private readonly IMapper _mapper;

        public EmployeesController(FitnessContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            var viewModel = new EmployeeIndexDataViewModel();
            viewModel.Employees = await _context.Employees.AsNoTracking().ToListAsync();
            ViewData["FirstNameSort"] = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";
            ViewData["LastNameSort"] = String.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "lastname";

            switch (sortOrder)
            {
                case "name_desc":
                    viewModel.Employees = viewModel.Employees.OrderByDescending(c => c.FirstName);
                    break;
                case "lastname_desc":
                    viewModel.Employees = viewModel.Employees.OrderByDescending(c => c.LastName);
                    break;
                case "lastname":
                    viewModel.Employees = viewModel.Employees.OrderBy(c => c.LastName);
                    break;
                default:
                    viewModel.Employees = viewModel.Employees.OrderBy(c => c.FirstName);
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.Employees = viewModel.Employees.Where(s => s.LastName.Contains(searchString)
                                               || s.FirstName.Contains(searchString));
            }

            return View(viewModel);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.Include(e => e.Gym)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            var employeeViewModel = _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(employeeViewModel);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewBag.Countries = new SelectList(Country.GetCountries(), "ID", "Name");
            ViewData["GymId"] = new SelectList(_context.Gyms, "GymId", "GymName");

            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Certification,Specialization,Id,FirstName,LastName,Birthday,Gender,Email,Country,City,GymId")] EmployeeViewModel model)
        {
            var employee = _mapper.Map<EmployeeViewModel, Employee>(model);
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Countries = new SelectList(Country.GetCountries(), "ID", "Name");
            var employeeVm = _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(employeeVm);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.SingleOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.Countries = new SelectList(Country.GetCountries(), "ID", "Name");
            ViewData["GymId"] = new SelectList(_context.Gyms, "GymId", "GymName");
            var employeeVm = _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(employeeVm);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Certification,Specialization,Id,FirstName,LastName,Birthday,Gender,Email,Country,City,GymId")] EmployeeViewModel model)
        {
            var employee = _mapper.Map<EmployeeViewModel, Employee>(model);

            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            ViewData["GymId"] =
                new SelectList(_context.WorkoutTypes, "GymId", "GymName", employee.GymId);
            ViewBag.Countries = new SelectList(Country.GetCountries(), "ID", "Name", employee.Country);
            var employeeVm = _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(employeeVm);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .SingleOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            var employeeVm = _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(employeeVm);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.SingleOrDefaultAsync(m => m.Id == id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
