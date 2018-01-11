using AutoMapper;
using FitnessTech.Data;
using FitnessTech.Data.Entities;
using FitnessTech.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class EmployeesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            var viewModel = new EmployeeIndexDataViewModel();
            viewModel.Employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
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
                viewModel.Employees = await _unitOfWork.EmployeeRepository.FindAllAsync(w => w.FullName.Contains(searchString));
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(EmployeeIndexDataViewModel viewModel)
        {
            List<Employee> selectedEmployees = new List<Employee>();

            if (viewModel.PersonalTrainer)
            {
                var result = _unitOfWork.EmployeeRepository.FindAll(c => c.Certification == Certification.PersonalTrainer);
                foreach (var res in result)
                {
                    selectedEmployees.Add(res);

                }
            }
            if (viewModel.Nutritionist)
            {
                var result = _unitOfWork.EmployeeRepository.FindAll(c => c.Certification == Certification.Nutritionist);
                foreach (var res in result)
                {
                    selectedEmployees.Add(res);

                }
            }
            if (viewModel.SportsConditioning)
            {
                var result = _unitOfWork.EmployeeRepository.FindAll(c => c.Specialization == Specialization.SportsConditioning);
                foreach (var res in result)
                {
                    selectedEmployees.Add(res);

                }
            }
            if (viewModel.WeightManagement)
            {
                var result = _unitOfWork.EmployeeRepository.FindAll(c => c.Specialization == Specialization.WeightManagement);
                foreach (var res in result)
                {
                    selectedEmployees.Add(res);

                }
            }
            if (viewModel.YouthFitness)
            {
                var result = _unitOfWork.EmployeeRepository.FindAll(c => c.Specialization == Specialization.YouthFitness);
                foreach (var res in result)
                {
                    selectedEmployees.Add(res);

                }
            }
            if (viewModel.SeniorFitness)
            {
                var result = _unitOfWork.EmployeeRepository.FindAll(c => c.Specialization == Specialization.SeniorFitness);
                foreach (var res in result)
                {
                    selectedEmployees.Add(res);

                }
            }

            viewModel.Employees = selectedEmployees.Distinct();


            return View(viewModel);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _unitOfWork.EmployeeRepository.GetBy(e => e.Id == id, e => e.Gym);
            if (employee == null)
            {
                return NotFound();
            }
            //var employeeViewModel =  _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewBag.Countries = new SelectList(Country.GetCountries(), "ID", "Name");
            ViewData["GymId"] = new SelectList(_unitOfWork.GymRepository.GetAll(), "GymId", "GymName");

            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Certification,Specialization,Id,FirstName,LastName,Birthday,Gender,Email,Country,City,GymId,AvatarImage")] EmployeeViewModel model)
        {
            var employee = _mapper.Map<EmployeeViewModel, Employee>(model);
            if (ModelState.IsValid)
            {

                if (model.AvatarImage != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.AvatarImage.CopyToAsync(memoryStream);
                        employee.AvatarImage = memoryStream.ToArray();
                    }
                }
               
                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                await _unitOfWork.EmployeeRepository.SaveAsync();
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

            var employeeTask = _unitOfWork.EmployeeRepository.GetBy(e => e.Id == id, e => e.Gym);
            var employee = await employeeTask;
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.Countries = new SelectList(Country.GetCountries(), "ID", "Name");
            ViewData["GymId"] = new SelectList(_unitOfWork.GymRepository.GetAll(), "GymId", "GymName");
            //var employeeVm = _mapper.Map<Employee, EmployeeViewModel>(employee);
            var employeeVm = new EmployeeViewModel();
            employeeVm.Birthday = employee.Birthday;
            employeeVm.Certification = employee.Certification;
            employeeVm.City = employee.City;
            employeeVm.Country = employee.Country;
            employeeVm.Customers = employee.Customers;
            employeeVm.Email = employee.Email;
            employeeVm.FirstName = employee.FirstName;
            employeeVm.Gender = employee.Gender;
            employeeVm.GymId = employee.GymId;
            employeeVm.LastName = employee.LastName;
            employeeVm.Specialization = employee.Specialization;
            employeeVm.LastName = employee.LastName;

            return View(employeeVm);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Certification,Specialization,Id,FirstName,LastName,Birthday,Gender,Email,Country,City,GymId,AvatarImage")] EmployeeViewModel model)
        {
            //var employee = _mapper.Map<EmployeeViewModel, Employee>(model);
            var employee = _unitOfWork.EmployeeRepository.Get(model.Id);

            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                employee.Birthday = model.Birthday;
                employee.Certification = model.Certification;
                employee.City = model.City;
                employee.Country = model.Country;
                employee.Customers = model.Customers;
                employee.Email = model.Email;
                employee.FirstName = model.FirstName;
                employee.Gender = model.Gender;
                employee.GymId = model.GymId;
                employee.LastName = model.LastName;
                employee.Specialization = model.Specialization;
                employee.LastName = model.LastName;
                if (model.AvatarImage != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.AvatarImage.CopyToAsync(memoryStream);
                        employee.AvatarImage = memoryStream.ToArray();
                    }
                }
                try
                {
                    _unitOfWork.EmployeeRepository.Update(employee);
                    await _unitOfWork.EmployeeRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!_unitOfWork.EmployeeRepository.Exists(employee))
                    if (!_unitOfWork.EmployeeRepository.Exists(employee.Id))
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
                new SelectList(_unitOfWork.GymRepository.GetAll(), "GymId", "GymName", employee.GymId);
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

            var employeeTask = _unitOfWork.EmployeeRepository.GetBy(e => e.Id == id, e=> e .Gym);
            var employee = await employeeTask;
            if (employee == null)
            {
                return NotFound();
            }
            //var employeeVm = _mapper.Map<Employee, EmployeeViewModel>(employee);
            var employeeVm = new EmployeeViewModel();
            employeeVm.Birthday = employee.Birthday;
            employeeVm.Certification = employee.Certification;
            employeeVm.City = employee.City;
            employeeVm.Country = employee.Country;
            employeeVm.Customers = employee.Customers;
            employeeVm.Email = employee.Email;
            employeeVm.FirstName = employee.FirstName;
            employeeVm.Gender = employee.Gender;
            employeeVm.GymId = employee.GymId;
            employeeVm.LastName = employee.LastName;
            employeeVm.Specialization = employee.Specialization;
            employeeVm.LastName = employee.LastName;

            return View(employeeVm);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetBy(e => e.Id == id, e => e.Gym);
             await _unitOfWork.EmployeeRepository.DeleteAsync(employee);
            await _unitOfWork.EmployeeRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

       
    }
}
