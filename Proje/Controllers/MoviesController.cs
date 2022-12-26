using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proje.Models;

namespace Proje.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ShowContext _context;

        public MoviesController(ShowContext context)
        {
            _context = context;
        }

        // GET: Movies
        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortBy, string search)
        {
            var movies = from a in _context.movies
                         select a;

            if (!string.IsNullOrEmpty(search))
            {
                movies = movies.Where(x => x.movieTitle.Contains(search));
            }

            switch (sortBy)
            {
                case "title":
                    movies = movies.OrderBy(x => x.movieTitle);
                    break;
                case "rating":
                    
                default:
                    movies = movies.OrderByDescending(x => x.movieRating);
                    break;

                case "year":
                    movies = movies.OrderByDescending(x => x.movieYear);
                    break;
                
                case "runningTime":
                    movies = movies.OrderByDescending(x => x.movieRunningTime);
                    break;
            }
            return View(await movies.ToListAsync());
        }

        // GET: Movies/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.movies == null)
            {
                return NotFound();
            }

            var movie = await _context.movies
                .FirstOrDefaultAsync(m => m.movieId == id);

            movie.movieCategoryArray = movie.movieCategories.Split(",");
            string[] categoryArray = new string[movie.movieCategoryArray.Length];
            int i = 0;
            foreach (var item in movie.movieCategoryArray)
            {
                categoryArray[i] = (from x in _context.categories
                                    where x.categoryId == Int32.Parse(item)
                                    select x.categoryName).FirstOrDefault();
                i++;
            }
            movie.movieCategories = String.Join(",", categoryArray);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        [Authorize(Roles ="admin")]
        public IActionResult Create()
        {
            Movie movie = new Movie();
            movie.categoryCollection = _context.categories.ToList();
            return View(movie);
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("movieId,movieTitle,movieYear,moviePoster,movieRating,movieStory,movieRunningTime,movieCategories,movieCategoryArray")] Movie movie)
        {
            movie.movieCategories = string.Join(",", movie.movieCategoryArray);
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.movies == null)
            {
                return NotFound();
            }

            var movie = await _context.movies.FindAsync(id);
            movie.movieCategoryArray = movie.movieCategories.Split(",");
            movie.categoryCollection = _context.categories.ToList();
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("movieId,movieTitle,movieYear,moviePoster,movieRating,movieStory,movieRunningTime,movieCategories")] Movie movie)
        {
            if (id != movie.movieId)
            {
                return NotFound();
            }

            movie.movieCategories = String.Join(",", movie.movieCategoryArray);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.movieId))
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
            return View(movie);
        }

        // GET: Movies/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.movies == null)
            {
                return NotFound();
            }

            var movie = await _context.movies
                .FirstOrDefaultAsync(m => m.movieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.movies == null)
            {
                return Problem("Entity set 'ShowContext.movies'  is null.");
            }
            var movie = await _context.movies.FindAsync(id);
            if (movie != null)
            {
                _context.movies.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
          return _context.movies.Any(e => e.movieId == id);
        }
    }
}
