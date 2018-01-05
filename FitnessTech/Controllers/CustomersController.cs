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
using FitnessTech.Repositories.Interfaces;

namespace FitnessTech.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        private readonly IMapper _mapper;

        public CustomersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: Customers
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            var viewModel = new CustomerIndexDataViewModel();
            viewModel.Customers = await _unitOfWork.CustomerRepository.GetAllAsync();

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
                viewModel.Customers = await _unitOfWork.CustomerRepository.FindAllAsync(w => w.FullName.Contains(searchString));

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

            var customer = await _unitOfWork.CustomerRepository.GetBy(c => c.Id == id, c => c.Employee, c => c.Gym);
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
            ViewData["GymId"] = new SelectList(_unitOfWork.GymRepository.GetAll(), "GymId", "GymName");
            ViewData["EmployeeId"] = new SelectList(_unitOfWork.EmployeeRepository.GetAll(), "Id", "FirstName");

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
                _unitOfWork.CustomerRepository.Add(customer);
                await _unitOfWork.CustomerRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GymId"] = new SelectList(_unitOfWork.GymRepository.GetAll(), "GymId", "GymName");
            ViewBag.Countries = new SelectList(Country.GetCountries(), "ID", "Name");
            ViewData["EmployeeId"] = new SelectList(_unitOfWork.EmployeeRepository.GetAll(), "Id", "FirstName");
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

            var customer = await _unitOfWork.CustomerRepository.GetAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewBag.Countries = new SelectList(Country.GetCountries(), "ID", "Name");
            ViewData["GymId"] = new SelectList(_unitOfWork.GymRepository.GetAll(), "GymId", "GymName");
            ViewData["EmployeeId"] = new SelectList(_unitOfWork.EmployeeRepository.GetAll(), "Id", "FirstName");
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
                    _unitOfWork.CustomerRepository.Update(customer);
                    await _unitOfWork.CustomerRepository.SaveAsync();
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
            ViewData["GymId"] = new SelectList(_unitOfWork.GymRepository.GetAll(), "GymId", "GymName", customer.Gym);
            ViewData["EmployeeId"] = new SelectList(_unitOfWork.EmployeeRepository.GetAll(), "Id", "FirstName", customer.Employee);
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

            var customer = await _unitOfWork.CustomerRepository.GetAsync(id);
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
            var customer = await _unitOfWork.CustomerRepository.GetAll().SingleOrDefaultAsync(m => m.Id == id);
            _unitOfWork.CustomerRepository.Delete(customer);
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _unitOfWork.CustomerRepository.GetAll().Any(e => e.Id == id);
        }
    }
}
