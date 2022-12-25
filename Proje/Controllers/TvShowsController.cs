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
    public class TvShowsController : Controller
    {
        private readonly ShowContext _context;

        public TvShowsController(ShowContext context)
        {
            _context = context;
        }

        // GET: TvShows
        public async Task<IActionResult> Index(string sortBy, string search)
        {
            var tvShows = from a in _context.tvShows
                         select a;

            if (!string.IsNullOrEmpty(search))
            {
                tvShows = tvShows.Where(x => x.showTitle.Contains(search));
            }

            switch (sortBy)
            {
                case "title":
                    tvShows = tvShows.OrderBy(x => x.showTitle);
                    break;



                case "year":
                    tvShows = tvShows.OrderByDescending(x => x.showStartYear);
                    break;
                default:
                case "rating":
                    tvShows = tvShows.OrderByDescending(x => x.showRating);
                    break;
                case "episodes":
                    tvShows = tvShows.OrderByDescending(x => x.showEpisodes);
                    break;
            }
            return View(await tvShows.ToListAsync());
        }

        // GET: TvShows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.tvShows == null)
            {
                return NotFound();
            }

            var tvShow = await _context.tvShows
                .FirstOrDefaultAsync(m => m.showId == id);

            tvShow.showCategoryArray = tvShow.showCategories.Split(",");
            string[] categoryArray = new string[tvShow.showCategoryArray.Length];
            int i = 0;
            foreach (var item in tvShow.showCategoryArray)
            {
                categoryArray[i] = (from x in _context.categories
                                    where x.categoryId == Int32.Parse(item)
                                    select x.categoryName).FirstOrDefault();
                i++;
            }
            tvShow.showCategories = String.Join(",", categoryArray);


            if (tvShow == null)
            {
                return NotFound();
            }

            return View(tvShow);
        }

        // GET: TvShows/Create
        public IActionResult Create()
        {
            TvShow show = new TvShow();
            show.categoryCollection = _context.categories.ToList();
            return View(show);
        }

        // POST: TvShows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("showId,showTitle,showPoster,showStartYear,showEndYear,showEpisodes,showRating,showStory,showCategories,showCategoryArray")] TvShow tvShow)
        {
            tvShow.showCategories = string.Join(",", tvShow.showCategoryArray);
            if (ModelState.IsValid)
            {
                _context.Add(tvShow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tvShow);
        }

        // GET: TvShows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.tvShows == null)
            {
                return NotFound();
            }

            var tvShow = await _context.tvShows.FindAsync(id);

            tvShow.showCategoryArray = tvShow.showCategories.Split(",");
            tvShow.categoryCollection = _context.categories.ToList();

            if (tvShow == null)
            {
                return NotFound();
            }
            return View(tvShow);
        }

        // POST: TvShows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("showId,showTitle,showPoster,showStartYear,showEndYear,showEpisodes,showRating,showStory,showCategories")] TvShow tvShow)
        {
            if (id != tvShow.showId)
            {
                return NotFound();
            }

            tvShow.showCategories = String.Join(",", tvShow.showCategoryArray);

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

        // GET: TvShows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.tvShows == null)
            {
                return NotFound();
            }

            var tvShow = await _context.tvShows
                .FirstOrDefaultAsync(m => m.showId == id);
            if (tvShow == null)
            {
                return NotFound();
            }

            return View(tvShow);
        }

        // POST: TvShows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.tvShows == null)
            {
                return Problem("Entity set 'ShowContext.tvShows'  is null.");
            }
            var tvShow = await _context.tvShows.FindAsync(id);
            if (tvShow != null)
            {
                _context.tvShows.Remove(tvShow);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TvShowExists(int id)
        {
          return _context.tvShows.Any(e => e.showId == id);
        }
    }
}
