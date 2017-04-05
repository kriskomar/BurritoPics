using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BurritoPicsDotNetCore.Models;
using BurritoPicsDotNetCore.ViewModels;

namespace BurritoPicsDotNetCore.Controllers
{
    public class PicsController : Controller
    {
        private readonly PicContext _context;

        public PicsController(PicContext context)
        {
            _context = context;    
        }

        // GET: Pics
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pic.ToListAsync());
        }

        // GET: Pics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pic = await _context.Pic
                .SingleOrDefaultAsync(m => m.Id == id);
            if (pic == null)
            {
                return NotFound();
            }

            return View(pic);
        }

        // GET: Pics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FileName,DateSubmitted,DateApproved,Approved,Name,Type")] Pic pic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pic);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pic);
        }

        // GET: Pics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pic = await _context.Pic.SingleOrDefaultAsync(m => m.Id == id);
            if (pic == null)
            {
                return NotFound();
            }
            return View(pic);
        }

        // POST: Pics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FileName,DateSubmitted,DateApproved,Approved,Name,Type")] Pic pic)
        {
            if (id != pic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PicExists(pic.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(pic);
        }

        // GET: Pics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pic = await _context.Pic
                .SingleOrDefaultAsync(m => m.Id == id);
            if (pic == null)
            {
                return NotFound();
            }

            return View(pic);
        }

        // POST: Pics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pic = await _context.Pic.SingleOrDefaultAsync(m => m.Id == id);
            _context.Pic.Remove(pic);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PicExists(int id)
        {
            return _context.Pic.Any(e => e.Id == id);
        }
    }
}
