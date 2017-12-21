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
    public class CustomersController : Controller
    {
        private readonly FitnessContext _context;
        private readonly IMapper _mapper;

        public CustomersController(FitnessContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Customers
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            var viewModel = new CustomerIndexDataViewModel();
            viewModel.Customers = await _context.Customers.ToListAsync();
            ViewData["FirstNameSort"] = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";
            ViewData["LastNameSort"] = String.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "lastname";

            switch (sortOrder)
            {
                case "name_desc":
                    viewModel.Customers = viewModel.Customers.OrderByDescending(c => c.FirstName);
                    break;
                case "lastname_desc":
                    viewModel.Customers = viewModel.Customers.OrderByDescending(c => c.LastName);
                    break;
                case "lastname":
                    viewModel.Customers = viewModel.Customers.OrderBy(c => c.LastName);
                    break;
                default:
                    viewModel.Customers = viewModel.Customers.OrderBy(c => c.FirstName);
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.Customers = viewModel.Customers.Where(c => c.FirstName.Contains(searchString)
                            || c.LastName.Contains(searchString));
            }


            return View(viewModel);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.Include(c => c.Gym).Include(c => c.Employee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            var customerViewModel = _mapper.Map<Customer, CustomerViewModel>(customer);

            return View(customerViewModel);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewBag.Countries = new SelectList(Country.GetCountries(), "ID", "Name");
            ViewData["GymId"] = new SelectList(_context.Gyms, "GymId", "GymName");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FirstName");

            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Goal,Weight,Height,ActivityLevel,BodyFat,Id,FirstName,LastName,Birthday,Gender,Email,Country,City,GymId,EmployeeId")] CustomerViewModel model)
        {
            var customer = _mapper.Map<CustomerViewModel, Customer>(model);
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GymId"] = new SelectList(_context.Gyms, "GymId", "GymName");
            ViewBag.Countries = new SelectList(Country.GetCountries(), "ID", "Name");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FirstName");
            var customerVm = _mapper.Map<Customer, CustomerViewModel>(customer);

            return View(customerVm);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewBag.Countries = new SelectList(Country.GetCountries(), "ID", "Name");
            ViewData["GymId"] = new SelectList(_context.Gyms, "GymId", "GymName");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FirstName");
            var customerVm = _mapper.Map<Customer, CustomerViewModel>(customer);
            return View(customerVm);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Goal,Weight,Height,ActivityLevel,BodyFat,Id,FirstName,LastName,Birthday,Gender,Email,Country,City,GymId,EmployeeId")] CustomerViewModel model)
        {
            var customer = _mapper.Map<CustomerViewModel, Customer>(model);
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            ViewBag.Countries = new SelectList(Country.GetCountries(), "ID", "Name", customer.Country);
            ViewData["GymId"] = new SelectList(_context.Gyms, "GymId", "GymName", customer.Gym);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FirstName", customer.Employee);
            var customerVM = _mapper.Map<Customer, CustomerViewModel>(customer);
            return View(customerVM);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            var customerVm = _mapper.Map<Customer, CustomerViewModel>(customer);
            return View(customerVm);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.Id == id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
