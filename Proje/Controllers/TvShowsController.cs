using System;
using System.Collections.Generic;
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
    public class TvShowsController : Controller
    {
        private readonly ShowContext _context;

        public TvShowsController(ShowContext context)
        {
            _context = context;
        }

        // GET: TvShows
        [AllowAnonymous]
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
        [AllowAnonymous]
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

            if (tvShow.showEndYear == null)
            {
                ViewData["endYear"] = "Current";
            }

            if (tvShow == null)
            {
                return NotFound();
            }

            return View(tvShow);
        }

        // GET: TvShows/Create
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("showId,showTitle,showPoster,showStartYear,showEndYear,showEpisodes,showRating,showStory,showCategories,showCategoryArray")] TvShow tvShow)
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.tvShows == null)
            {
                return NotFound();
            }

            var tvShow = await _context.tvShows
                .FirstOrDefaultAsync(m => m.showId == id);

            if (tvShow.showEndYear == null)
            {
                ViewData["endYear"] = "Current";
            }

            if (tvShow == null)
            {
                return NotFound();
            }

            return View(tvShow);
        }

        // POST: TvShows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
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


        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> addShow(int? showId)
        {
            TvShowUser showUser = new TvShowUser();
            var show = await _context.tvShows.FindAsync(showId);


            if (show == null)
            {
                return NotFound();
            }

            showUser.showId = (int)showId;
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            showUser.userId = userId;


            return View(showUser);
        }

        [Authorize(Roles = "user,admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addShow(int showId, TvShowUser showUser)
        {

            if (showId != showUser.showId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                _context.Add(showUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(myShowList));
            }
            
            return View();
        }

        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> myShowList(string sortBy, string search)
        {

            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var showUser = (from au in _context.tvShow_Users
                             join a in _context.tvShows
                             on au.showId equals a.showId
                             select au).Where(x => x.userId == userId);

            if (showUser != null)
                foreach (var item in showUser)
                {
                    item.tvShow = await _context.tvShows.FindAsync(item.showId);
                }

            if (!string.IsNullOrEmpty(search))
            {
                showUser = showUser.Where(x => x.tvShow.showTitle.Contains(search) && x.userId == userId);
            }

            switch (sortBy)
            {
                case "title":
                    showUser = showUser.Where(x => x.userId == userId).OrderBy(x => x.tvShow.showTitle);
                    break;

                case "year":
                    showUser = showUser.Where(x => x.userId == userId).OrderByDescending(x => x.tvShow.showStartYear);
                    break;

                default:
                case "rating":
                    showUser = showUser.Where(x => x.userId == userId).OrderByDescending(x => x.userRating);
                    break;

                case "episodes":
                    showUser = showUser.Where(x => x.userId == userId).OrderByDescending(x => x.tvShow.showEpisodes);
                    break;
                case "planToWatch":
                    showUser = showUser.Where(x => x.userId == userId).Where(x => x.watchStatus.Equals("Plan to watch"));
                    break;
                case "watching":
                    showUser = showUser.Where(x => x.userId == userId).Where(x => x.watchStatus.Equals("Watching"));
                    break;
                case "completed":
                    showUser = showUser.Where(x => x.userId == userId).Where(x => x.watchStatus.Equals("Completed"));
                    break;
            }

            return View(await showUser.ToListAsync());
        }


        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> deleteShow(int? showId, string? userId)
        {
            if (_context.tvShow_Users == null)
            {
                return Problem("Entity set 'ShowContext.show_Users'  is null.");
            }
            var showUser = (from a in _context.tvShow_Users
                             where a.userId == userId
                             select a).Where(x => x.showId == showId).FirstOrDefault();

            if (showUser != null)
            {
                _context.tvShow_Users.Remove(showUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(myShowList));
        }


        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> editShow(int? showId)
        {
            if (showId == null || _context.tvShow_Users == null)
            {
                return NotFound();
            }

            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }


            var showUser = (from a in _context.tvShow_Users
                             where a.userId == userId
                             select a).Where(x => x.showId == showId).FirstOrDefault();

            showUser.userId = userId;

            return View(showUser);
        }

        [Authorize(Roles = "user,admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> editShow(int showId, TvShowUser showUser)
        {

            if (showId != showUser.showId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(showUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(myShowList));
            }
            
            return View();
        }


        private bool TvShowExists(int id)
        {
          return _context.tvShows.Any(e => e.showId == id);
        }
    }
}
