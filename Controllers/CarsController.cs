using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using SammysAuto.Data;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using SammysAuto.ViewModel;
using SammysAuto.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.EntityFrameworkCore;

namespace SammysAuto.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CarsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: /<controller>/
        public async Task <IActionResult> Index(string userId = null)
        {
            if(userId == null)
            {   // Only executes when a customer user logs in
                userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var model = new CarAndCustomerViewModel
            { 
                Cars = _db.Cars.Where(c=> c.UserId == userId),
                UserObj = _db.Users.FirstOrDefault(u=>u.Id==userId)         
            };

            return View(model);
        }

        // Create GET:
        public IActionResult Create(string userId)
        {
            Car carObj = new Car
            {
                Year = DateTime.Now.Year,
                UserId = userId
            };

            return View(carObj);
        }

        // Create POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (Car car)
        {
            if(ModelState.IsValid)
            {
                _db.Add(car);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { UserId = car.UserId });
            }

            return View(car);
        }

        // Details GET:
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var car = await _db.Cars.Include(c => c.ApplicationUser).SingleOrDefaultAsync(m => m.Id == id);

            if(car == null)
            {
                NotFound();
            }

            return View(car);
        }

        // Edit GET:
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _db.Cars.Include(c => c.ApplicationUser).SingleOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                NotFound();
            }

            return View(car);
        }

        // Edit POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Car car)
        {
            if(id != car.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                _db.Update(car);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { userId = car.UserId });
            }

            return View(car);
        }

        // Delete GET:
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _db.Cars.Include(c => c.ApplicationUser).SingleOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                NotFound();
            }

            return View(car);
        }

        // Delete POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> DeleteConfirmed(int id)
        {
            var car = await _db.Cars.SingleOrDefaultAsync(c => c.Id == id);
            if(car == null)
            {
                return NotFound();
            }

            _db.Cars.Remove(car);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new {userId = car.UserId});
        }

		protected override void Dispose(bool disposing)
		{
            if(disposing)
            {
                _db.Dispose();
            }
        }
	}
}
