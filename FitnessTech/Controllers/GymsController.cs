using AutoMapper;
using FitnessTech.Data;
using FitnessTech.Data.Entities;
using FitnessTech.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Data.Helpers;
using FitnessTech.Repositories.Interfaces;

namespace FitnessTech.Controllers
{
    public class GymsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GymsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: Gyms
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            var viewModel = new GymIndexViewModel();
            viewModel.Gyms = await _unitOfWork.GymRepository.GetAllAsync();
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
                viewModel.Gyms = await _unitOfWork.GymRepository.FindAllAsync(w => w.GymName.Contains(searchString));

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

            var gym = await _unitOfWork.GymRepository.GetBy(g => g.GymId == id, g => g.Employees, g=> g.Customers);
            if (gym == null)
            {
                return NotFound();
            }
            //var model = _mapper.Map<Gym, GymViewModel>(gym);
            return View(gym);
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
        public async Task<IActionResult> Create([Bind("GymId,GymName,Description,Website,Email,Country,City,PostalCode,Street,PhoneNumber,AvatarImage")] GymViewModel model)
        {
            var gym = _mapper.Map<GymViewModel, Gym>(model);
            if (ModelState.IsValid)
            {
                if (model.AvatarImage != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.AvatarImage.CopyToAsync(memoryStream);
                        gym.AvatarImage = memoryStream.ToArray();
                    }
                }
                _unitOfWork.GymRepository.Add(gym);
                await _unitOfWork.GymRepository.SaveAsync();
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

            var gym = await _unitOfWork.GymRepository.GetBy(g => g.GymId == id, g => g.Employees, g => g.Customers);
            if (gym == null)
            {
                return NotFound();
            }
            ViewBag.Countries = new SelectList(Country.GetCountries(), "ID", "Name");
            //var gymVm = _mapper.Map<Gym, GymViewModel>(gym);
            var gymVm = new GymViewModel();
            gymVm.City = gym.City;
            gymVm.Country = gym.Country;
            gymVm.Customers = gym.Customers;
            gymVm.Description = gym.Description;
            gymVm.Email = gym.Email;
            gymVm.Employees = gym.Employees;
            gymVm.GymName = gym.GymName;
            gymVm.Email = gym.Email;
            gymVm.Employees = gym.Employees;
            gymVm.GymName= gym.GymName;
            gymVm.PhoneNumber = gym.PhoneNumber;
            gymVm.PostalCode = gym.PostalCode;
            gymVm.Street = gym.Street;
            gymVm.Website = gym.Website;
            gymVm.GymId = gym.GymId;

            return View(gymVm);
        }

        // POST: Gyms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GymId,GymName,Description,Website,Email,Country,City,PostalCode,Street,PhoneNumber,AvatarImage")] GymViewModel model)
        {
            //var gym = _mapper.Map<GymViewModel, Gym>(model);
            var gym = _unitOfWork.GymRepository.Get(model.GymId);


            if (id != gym.GymId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                gym.City = model.City;
                gym.Country = model.Country;
                gym.Customers = model.Customers;
                gym.Description = model.Description;
                gym.Email = model.Email;
                gym.Employees = model.Employees;
                gym.GymName = model.GymName;
                gym.Email = model.Email;
                gym.Employees = model.Employees;
                gym.GymName = model.GymName;
                gym.PhoneNumber = model.PhoneNumber;
                gym.PostalCode = model.PostalCode;
                gym.Street = model.Street;
                gym.Website = model.Website;
                try
                {
                    if (model.AvatarImage != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.AvatarImage.CopyToAsync(memoryStream);
                            gym.AvatarImage = memoryStream.ToArray();
                        }
                    }
                    _unitOfWork.GymRepository.Update(gym);
                    await _unitOfWork.GymRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.GymRepository.Exists(gym.GymId))
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

            var gym = await _unitOfWork.GymRepository.GetAsync(id);
            if (gym == null)
            {
                return NotFound();
            }
            //var gymVm = _mapper.Map<Gym, GymViewModel>(gym);
            var gymVm = new GymViewModel();
            gymVm.City = gym.City;
            gymVm.Country = gym.Country;
            gymVm.Customers = gym.Customers;
            gymVm.Description = gym.Description;
            gymVm.Email = gym.Email;
            gymVm.Employees = gym.Employees;
            gymVm.GymName = gym.GymName;
            gymVm.Email = gym.Email;
            gymVm.Employees = gym.Employees;
            gymVm.GymName = gym.GymName;
            gymVm.PhoneNumber = gym.PhoneNumber;
            gymVm.PostalCode = gym.PostalCode;
            gymVm.Street = gym.Street;
            gymVm.Website = gym.Website;

            return View(gymVm);
        }

        // POST: Gyms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gym = await _unitOfWork.GymRepository.GetAsync(id);
            _unitOfWork.GymRepository.Delete(gym);
            await _unitOfWork.GymRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
