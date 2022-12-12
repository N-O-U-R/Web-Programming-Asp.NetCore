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
    public class AnimesController : Controller
    {
        ShowContext _context = new ShowContext();
        // GET: Animes
        public async Task<IActionResult> Index()
        {
              return View(await _context.animes.ToListAsync());
        }

        // GET: Animes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.animes == null)
            {
                return NotFound();
            }

            var anime = await _context.animes
                .FirstOrDefaultAsync(m => m.animeId == id);
            if (anime == null)
            {
                return NotFound();
            }

            return View(anime);
        }

        // GET: Animes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Animes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("animeId,animeTitle,animePoster,animeRating,animeEpisodes,animeStartYear,animeEndYear,animeStory")] Anime anime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(anime);
        }

        // GET: Animes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.animes == null)
            {
                return NotFound();
            }

            var anime = await _context.animes.FindAsync(id);
            if (anime == null)
            {
                return NotFound();
            }
            return View(anime);
        }

        // POST: Animes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("animeId,animeTitle,animePoster,animeRating,animeEpisodes,animeStartYear,animeEndYear,animeStory")] Anime anime)
        {
            if (id != anime.animeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimeExists(anime.animeId))
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
            return View(anime);
        }

        // GET: Animes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.animes == null)
            {
                return NotFound();
            }

            var anime = await _context.animes
                .FirstOrDefaultAsync(m => m.animeId == id);
            if (anime == null)
            {
                return NotFound();
            }

            return View(anime);
        }

        // POST: Animes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.animes == null)
            {
                return Problem("Entity set 'ShowContext.animes'  is null.");
            }
            var anime = await _context.animes.FindAsync(id);
            if (anime != null)
            {
                _context.animes.Remove(anime);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimeExists(int id)
        {
          return _context.animes.Any(e => e.animeId == id);
        }
    }
}
