using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2_0.Data;
using Garage2_0.Models;
using Garage2_0.Models.ViewModels;
using System.Drawing;
using Microsoft.AspNetCore.Routing.Patterns;

namespace Garage2_0.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly Garage2_0Context _context;

        public ParkedVehiclesController(Garage2_0Context context)
        {
            _context = context;
        }

        private async Task<IEnumerable<SelectListItem>> ColorsAsync()
        {
            return await _context.ParkedVehicle
                .Select(m => m.Color)
                .Distinct()
                .Select(m => new SelectListItem
                {
                    Text = m.ToString(),
                    Value = m.ToString()
                })
                .ToListAsync();
        }


        // GET: ParkedVehicles
        public async Task<IActionResult> Index(IndexViewModel viewModel, string sortOrder)
        {
            ViewData["VTypeSortParm"] = sortOrder == "VType" ? "vtype_desc" : "VType";
            ViewData["RegNrSortParm"] = sortOrder == "RegNr" ? "regnr_desc" : "RegNr";
            ViewData["ArrivalSortParm"] = sortOrder == "arrival_desc" ? "Arrival" : "arrival_desc";

            var parkedVehicles = string.IsNullOrWhiteSpace(viewModel.RegistrationNumber) ?
                            _context.ParkedVehicle :
                           _context.ParkedVehicle.Where(m => m.RegistrationNumber.StartsWith(viewModel.RegistrationNumber.ToUpper()));

           parkedVehicles = string.IsNullOrWhiteSpace(viewModel.Color) ?
                          parkedVehicles :
                         parkedVehicles.Where(m => m.Color == viewModel.Color);

            switch (sortOrder)
            {
                case "VType":
                    parkedVehicles = parkedVehicles.OrderBy(s => s.VType);
                    break;
                case "vtype_desc":
                    parkedVehicles = parkedVehicles.OrderByDescending(s => s.VType);
                    break;
                case "RegNr":
                    parkedVehicles = parkedVehicles.OrderBy(s => s.RegistrationNumber);
                    break;
                case "arrival_desc":
                    parkedVehicles = parkedVehicles.OrderByDescending(s => s.Arrival);
                    break;
                case "regnr_desc":
                    parkedVehicles = parkedVehicles.OrderByDescending(s => s.RegistrationNumber);
                    break;
                default:
                    parkedVehicles = parkedVehicles.OrderBy(s => s.Arrival);
                    break;
            }

            var model = new IndexViewModel()
            {
                ParkedVehicles = await parkedVehicles.ToListAsync(),
                RegistrationNumber = viewModel.RegistrationNumber,
                Color = viewModel.Color,
                Colors = await ColorsAsync()
            };
            return View(nameof(Index), model);
        }


        // GET: ParkedVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,VType,Wheels,RegistrationNumber,Manufacturer,Arrival,Color,VehicleModel")] ParkedVehicle parkedVehicle)
        {
            //parkedVehicle.Arrival = DateTime.Now;

            parkedVehicle.RegistrationNumber = parkedVehicle.RegistrationNumber.ToUpper();
            parkedVehicle.Manufacturer = char.ToUpper(parkedVehicle.Manufacturer[0]) + parkedVehicle.Manufacturer.Substring(1);
            parkedVehicle.Color = char.ToUpper(parkedVehicle.Color[0]) + parkedVehicle.Color.Substring(1);
            parkedVehicle.VehicleModel = char.ToUpper(parkedVehicle.VehicleModel[0]) + parkedVehicle.VehicleModel.Substring(1);

            var found = _context.ParkedVehicle.FirstOrDefault(p => p.RegistrationNumber == parkedVehicle.RegistrationNumber);

            if (found != null)
            {
                ModelState.AddModelError("RegistrationNumber", "Registration number already exists");
            }

            if (ModelState.IsValid)
            {
                _context.Add(parkedVehicle);
                await _context.SaveChangesAsync();
                TempData["message"] = $"Reg Nr: {parkedVehicle.RegistrationNumber} is parked!";
                return RedirectToAction(nameof(Index));
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,VType,Wheels,RegistrationNumber,Manufacturer,Arrival,Color,VehicleModel")] ParkedVehicle parkedVehicle)
        {
            if (id != parkedVehicle.ID)
            {
                return NotFound();
            }

            parkedVehicle.RegistrationNumber = parkedVehicle.RegistrationNumber.ToUpper();
            parkedVehicle.Manufacturer = char.ToUpper(parkedVehicle.Manufacturer[0]) + parkedVehicle.Manufacturer.Substring(1);
            parkedVehicle.Color = char.ToUpper(parkedVehicle.Color[0]) + parkedVehicle.Color.Substring(1);
            parkedVehicle.VehicleModel = char.ToUpper(parkedVehicle.VehicleModel[0]) + parkedVehicle.VehicleModel.Substring(1);

            var found = _context.ParkedVehicle.FirstOrDefault(p => (p.RegistrationNumber == parkedVehicle.RegistrationNumber) && (p.ID != parkedVehicle.ID));

            if (found != null)
            {
                    ModelState.AddModelError("RegistrationNumber", "Registration number already exists");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkedVehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkedVehicleExists(parkedVehicle.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["message"] = $"Reg Nr: {parkedVehicle.RegistrationNumber} has been updated!";
                return RedirectToAction(nameof(Index));
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            var arrival = parkedVehicle.Arrival;
            var checkout = DateTime.Now;
            var realTime = (checkout - arrival).TotalSeconds / 3600;
            var chargeTime = (int)Math.Ceiling(realTime);
            var model = new ReceiptViewModel
            {
                RegistrationNumber = parkedVehicle.RegistrationNumber,
                Arrival = arrival,
                CheckOut = checkout,
                ParkingTime = chargeTime,
                Price = chargeTime * 80
            };

            _context.ParkedVehicle.Remove(parkedVehicle);
            await _context.SaveChangesAsync();
            return View("Receipt", model);
        }

        private bool ParkedVehicleExists(int id)
        {
            return _context.ParkedVehicle.Any(e => e.ID == id);
        }
    }
}
