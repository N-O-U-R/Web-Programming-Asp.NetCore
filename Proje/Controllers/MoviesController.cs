using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
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
        public async Task<IActionResult> Edit(int id, [Bind("movieId,movieTitle,movieYear,moviePoster,movieRating,movieStory,movieRunningTime,movieCategories,movieCategoryArray")] Movie movie)
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


        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> addMovie(int? movieId)
        {
            MovieUser movieUser = new MovieUser();
            var movie = await _context.movies.FindAsync(movieId);


            if (movie == null)
            {
                return NotFound();
            }

            movieUser.movieId = (int)movieId;
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            movieUser.userId = userId;


            return View(movieUser);
        }

        [Authorize(Roles = "user,admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addMovie(int movieId, MovieUser movieUser)
        {

            if (movieId != movieUser.movieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                _context.Add(movieUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(myMovieList));
            }
            
            return View();
        }

        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> myMovieList(string sortBy, string search)
        {

            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var movieUser = (from au in _context.movie_Users
                             join a in _context.movies
                             on au.movieId equals a.movieId
                             select au).Where(x => x.userId == userId);

            if (movieUser != null)
                foreach (var item in movieUser)
                {
                    item.movie = await _context.movies.FindAsync(item.movieId);
                }

            if (!string.IsNullOrEmpty(search))
            {
                movieUser = movieUser.Where(x => x.movie.movieTitle.Contains(search) && x.userId == userId);
            }

            switch (sortBy)
            {
                case "title":
                    movieUser = movieUser.Where(x => x.userId == userId).OrderBy(x => x.movie.movieTitle);
                    break;

                case "year":
                    movieUser = movieUser.Where(x => x.userId == userId).OrderByDescending(x => x.movie.movieYear);
                    break;
                    
                default:
                case "rating":
                    movieUser = movieUser.Where(x => x.userId == userId).OrderByDescending(x => x.userRating);
                    break;

                case "runningTime":
                    movieUser = movieUser.Where(x => x.userId == userId).OrderByDescending(x => x.movie.movieRunningTime);
                    break;
                case "planToWatch":
                    movieUser = movieUser.Where(x => x.userId == userId).Where(x => x.watchStatus.Equals("Plan to watch"));
                    break;
                
                case "completed":
                    movieUser = movieUser.Where(x => x.userId == userId).Where(x => x.watchStatus.Equals("Completed"));
                    break;
            }

            return View(await movieUser.ToListAsync());
        }


        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> deleteMovie(int? movieId, string? userId)
        {
            if (_context.movie_Users == null)
            {
                return Problem("Entity set 'ShowContext.movie_Users'  is null.");
            }
            var movieUser = (from a in _context.movie_Users
                             where a.userId == userId
                             select a).Where(x => x.movieId == movieId).FirstOrDefault();

            if (movieUser != null)
            {
                _context.movie_Users.Remove(movieUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(myMovieList));
        }


        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> editMovie(int? movieId)
        {
            if (movieId == null || _context.movie_Users == null)
            {
                return NotFound();
            }

            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }


            var movieUser = (from a in _context.movie_Users
                             where a.userId == userId
                             select a).Where(x => x.movieId == movieId).FirstOrDefault();

            movieUser.userId = userId;

            return View(movieUser);
        }

        [Authorize(Roles = "user,admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> editMovie(int movieId, MovieUser movieUser)
        {

            if (movieId != movieUser.movieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(movieUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(myMovieList));
            }
            
            return View();
        }


        private bool MovieExists(int id)
        {
          return _context.movies.Any(e => e.movieId == id);
        }
    }
}
