using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroepSpace2024.Data;
using GroepSpace2024.Models;

namespace GroepSpace2024.Controllers
{
    public class GroepsController : Controller
    {
        private readonly MyDatabaseContext _context;

        public GroepsController(MyDatabaseContext context)
        {
            _context = context;
        }

        // GET: Groeps
        public async Task<IActionResult> Index()
        {
            return View(await _context.Groeps.ToListAsync());
        }

        // GET: Groeps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groep = await _context.Groeps
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groep == null)
            {
                return NotFound();
            }

            return View(groep);
        }

        // GET: Groeps/Create
        public IActionResult Create()
        {
            return View(new Groep());
        }

        // POST: Groeps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Started,Ended")] Groep groep)
        {
            if (ModelState.IsValid)
            {
                groep.Started = DateTime.Now;
                groep.Ended = DateTime.MaxValue;
                await _context.SaveChangesAsync();
                _context.Add(groep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(groep);
        }

        // GET: Groeps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groep = await _context.Groeps.FindAsync(id);
            if (groep == null)
            {
                return NotFound();
            }
            return View(groep);
        }

        // POST: Groeps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Started,Ended")] Groep groep)
        {
            if (id != groep.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroepExists(groep.Id))
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
            return View(groep);
        }

        // GET: Groeps/Delete/5
        public async Task<IActionResult> Delete(int? id) //DeleteConfirmed
        {
            if (id == null)
            {
                return NotFound();
            }

            var groep = await _context.Groeps
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groep == null)
            {
                return NotFound();
            }

            return View(groep);
        }

        // POST: Groeps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groep = await _context.Groeps.FindAsync(id);
            if (groep != null)
            {
                //_context.Groeps.Remove(groep);
                
                 //Vervang bovenste met dit (zoals Waldo het heeft gedaan)
                groep.Ended = DateTime.Now;
                _context.Groeps.Update(@groep);
                
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroepExists(int id)
        {
            return _context.Groeps.Any(e => e.Id == id);
        }
    }
}
