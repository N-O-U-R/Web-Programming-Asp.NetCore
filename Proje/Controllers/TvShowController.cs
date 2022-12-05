using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proje.Models;

namespace Proje.Controllers
{
    public class TvShowController : Controller
    {
        ShowContext _context = new ShowContext();
        // GET: TvShow
        public async Task<IActionResult> Index()
        {
              return View(await _context.Shows.ToListAsync());
        }

        // GET: TvShow/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Shows == null)
            {
                return NotFound();
            }

            var tvShow = await _context.Shows
                .FirstOrDefaultAsync(m => m.showId == id);
            if (tvShow == null)
            {
                return NotFound();
            }

            return View(tvShow);
        }

        // GET: TvShow/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TvShow/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("showId,showTitle,showPoster,showStartYear,showEndYear,showEpisodes,showRating,showStory")] TvShow tvShow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tvShow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tvShow);
        }

        // GET: TvShow/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Shows == null)
            {
                return NotFound();
            }

            var tvShow = await _context.Shows.FindAsync(id);
            if (tvShow == null)
            {
                return NotFound();
            }
            return View(tvShow);
        }

        // POST: TvShow/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("showId,showTitle,showPoster,showStartYear,showEndYear,showEpisodes,showRating,showStory")] TvShow tvShow)
        {
            if (id != tvShow.showId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tvShow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TvShowExists(tvShow.showId))
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
            return View(tvShow);
        }

        // GET: TvShow/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Shows == null)
            {
                return NotFound();
            }

            var tvShow = await _context.Shows
                .FirstOrDefaultAsync(m => m.showId == id);
            if (tvShow == null)
            {
                return NotFound();
            }

            return View(tvShow);
        }

        // POST: TvShow/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Shows == null)
            {
                return Problem("Entity set 'ShowContext.Shows'  is null.");
            }
            var tvShow = await _context.Shows.FindAsync(id);
            if (tvShow != null)
            {
                _context.Shows.Remove(tvShow);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TvShowExists(int id)
        {
          return _context.Shows.Any(e => e.showId == id);
        }
    }
}
