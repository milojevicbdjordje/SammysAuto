using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using SammysAuto.Data;
using System.Runtime.CompilerServices;
using SammysAuto.Models;
using Microsoft.EntityFrameworkCore;

namespace SammysAuto.Controllers
{
    public class ServiceTypesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ServiceTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_db.ServiceTypes.ToList());
        }

        // GET: ServiceTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServiceTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceType serviceType)
        {
            if(ModelState.IsValid)
            {
                _db.Add(serviceType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceType);
        }

        //Details: ServiceTypes/Details/1
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var serviceType = await _db.ServiceTypes.SingleOrDefaultAsync(m => m.Id == id);

            if( serviceType == null)
            {
                return NotFound();
            }

            return View(serviceType);
        }

        //Details: ServiceTypes/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceType = await _db.ServiceTypes.SingleOrDefaultAsync(m => m.Id == id);

            if (serviceType == null)
            {
                return NotFound();
            }

            return View(serviceType);
        }

        // POST: ServiceTypes/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ServiceType serviceType)
        {
            if(id != serviceType.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                _db.Update(serviceType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(serviceType);
        }

        // POST: ServiceTypes/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveServiceType(int? id)
        {
            var serviceType = await _db.ServiceTypes.SingleOrDefaultAsync(m => m.Id == id);
            _db.ServiceTypes.Remove(serviceType);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }



        //Details: ServiceTypes/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceType = await _db.ServiceTypes.SingleOrDefaultAsync(m => m.Id == id);

            if (serviceType == null)
            {
                return NotFound();
            }

            return View(serviceType);
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
