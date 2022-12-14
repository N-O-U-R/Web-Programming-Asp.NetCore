using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proje.Models;

namespace Proje.Controllers
{
    public class AnimesController : Controller
    {
        private readonly ShowContext _context;

        public AnimesController(ShowContext context)
        {
            _context = context;
        }



        [AllowAnonymous]
        // GET: Animes
        public async Task<IActionResult> Index(string sortBy, string search)
        {
            var animes = from a in _context.animes
                         select a;

            if (!string.IsNullOrEmpty(search))
            {
                animes = animes.Where(x => x.animeTitle.Contains(search));
            }

            switch (sortBy)
            {
                case "title":
                    animes = animes.OrderBy(x => x.animeTitle);
                    break;
                case "year":
                    animes = animes.OrderByDescending(x => x.animeStartYear);
                    break;
                default:
                case "rating":
                    animes = animes.OrderByDescending(x => x.animeRating);
                    break;
                case "episodes":
                    animes = animes.OrderByDescending(x => x.animeEpisodes);
                    break;
            }

            return View(await animes.ToListAsync());
        }

        // GET: Animes/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null || _context.animes == null)
            {
                return NotFound();
            }

            var anime = await _context.animes
                .FirstOrDefaultAsync(m => m.animeId == id);

            anime.animeCategoryArray = anime.animeCategories.Split(",");
            string[] categoryArray = new string[anime.animeCategoryArray.Length];
            int i = 0;
            foreach (var item in anime.animeCategoryArray)
            {
                categoryArray[i] = (from x in _context.categories
                                    where x.categoryId == Int32.Parse(item)
                                    select x.categoryName).FirstOrDefault();
                i++;
            }

            if (anime.animeEndYear == null)
            {
                ViewData["endYear"] = "Current";
            }
            anime.animeCategories = String.Join(",", categoryArray);
            if (anime == null)
            {
                return NotFound();
            }

            return View(anime);
        }

        // GET: Animes/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            Anime anime = new Anime();

            anime.categoryCollection = _context.categories.ToList();
            return View(anime);
        }

        // POST: Animes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("animeId,animeTitle,animePoster,animeRating,animeEpisodes,animeStartYear,animeEndYear,animeStory,animeCategories,animeCategoryArray")] Anime anime)
        {
            if (anime.animeCategoryArray == null)
            {
                return View("Error");
            }

            anime.animeCategories = string.Join(",", anime.animeCategoryArray);


            if (ModelState.IsValid)
            {
                _context.Add(anime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

            TempData["addError"] = messages;
            return View("addError");
        }

        // GET: Animes/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.animes == null)
            {
                return NotFound();
            }

            var anime = await _context.animes.FindAsync(id);
            anime.animeCategoryArray = anime.animeCategories.Split(",");
            anime.categoryCollection = _context.categories.ToList();
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("animeId,animeTitle,animePoster,animeRating,animeEpisodes,animeStartYear,animeEndYear,animeStory,animeCategories,animeCategoryArray")] Anime anime)
        {
            if (id != anime.animeId)
            {
                return NotFound();
            }
            anime.animeCategories = string.Join(",", anime.animeCategoryArray);
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.animes == null)
            {
                return NotFound();
            }

            var anime = await _context.animes
                .FirstOrDefaultAsync(m => m.animeId == id);

            anime.animeCategoryArray = anime.animeCategories.Split(",");
            string[] categoryArray = new string[anime.animeCategoryArray.Length];
            int i = 0;
            foreach (var item in anime.animeCategoryArray)
            {
                categoryArray[i] = (from x in _context.categories
                                    where x.categoryId == Int32.Parse(item)
                                    select x.categoryName).FirstOrDefault();
                i++;
            }
            anime.animeCategories = String.Join(",", categoryArray);

            if (anime.animeEndYear == null)
            {
                ViewData["endYear"] = "Current";
            }

            if (anime == null)
            {
                return NotFound();
            }

            return View(anime);
        }
        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> addAnime(int? animeId)
        {
            AnimeUser animeUser = new AnimeUser();
            var anime = await _context.animes.FindAsync(animeId);


            if (anime == null)
            {
                return NotFound();
            }

            animeUser.animeId = (int)animeId;
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            animeUser.userId = userId;


            return View(animeUser);
        }

        [Authorize(Roles = "user,admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addAnime(int animeId, AnimeUser animeUser)
        {

            if (animeId != animeUser.animeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                _context.Add(animeUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(myAnimeList));
            }
            string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
            TempData["modelerror"] = messages;
            return View("addError");
        }

        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> myAnimeList(string sortBy, string search)
        {

            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var animeUser = (from au in _context.anime_Users
                             join a in _context.animes
                             on au.animeId equals a.animeId
                             select au).Where(x => x.userId == userId);

            if (animeUser != null)
                foreach (var item in animeUser)
                {
                    item.anime = await _context.animes.FindAsync(item.animeId);
                }

            if (!string.IsNullOrEmpty(search))
            {
                animeUser = animeUser.Where(x => x.anime.animeTitle.Contains(search)&& x.userId == userId);
            }

            switch (sortBy)
            {
                case "title":
                    animeUser = animeUser.Where(x => x.userId == userId).OrderBy(x => x.anime.animeTitle);
                    break;

                case "year":
                    animeUser = animeUser.Where(x => x.userId == userId).OrderByDescending(x => x.anime.animeStartYear);
                    break;

                default:
                case "rating":
                    animeUser = animeUser.Where(x=>x.userId==userId).OrderByDescending(x => x.userRating);
                    break;

                case "episodes":
                    animeUser = animeUser.Where(x => x.userId == userId).OrderByDescending(x => x.anime.animeEpisodes);
                    break;
                case "planToWatch":
                    animeUser = animeUser.Where(x => x.userId == userId).Where(x => x.watchStatus.Equals("Plan to watch"));
                    break;
                case "watching":
                    animeUser = animeUser.Where(x => x.userId == userId).Where(x => x.watchStatus.Equals("Watching"));
                    break;
                case "completed":
                    animeUser = animeUser.Where(x => x.userId == userId).Where(x => x.watchStatus.Equals("Completed"));
                    break;
            }

            return View(await animeUser.ToListAsync());
        }


        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> deleteAnime(int? animeId, string? userId)
        {
            if (_context.anime_Users == null)
            {
                return Problem("Entity set 'ShowContext.anime_Users'  is null.");
            }
            var animeUser = (from a in _context.anime_Users
                             where a.userId == userId
                             select a).Where(x => x.animeId == animeId).FirstOrDefault();

            if (animeUser != null)
            {
                _context.anime_Users.Remove(animeUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(myAnimeList));
        }


        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> editAnime(int? animeId)
        {
            if (animeId == null || _context.anime_Users == null)
            {
                return NotFound();
            }

            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }


            var animeUser = (from a in _context.anime_Users
                             where a.userId == userId
                             select a).Where(x => x.animeId == animeId).FirstOrDefault();

            animeUser.userId = userId;

            return View(animeUser);
        }

        [Authorize(Roles = "user,admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> editAnime(int animeId, AnimeUser animeUser)
        {

            if (animeId != animeUser.animeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(animeUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(myAnimeList));
            }
            string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
            TempData["modelerror"] = messages;
            return View("addError");
        }

        private bool AnimeExists(int id)
        {
            return _context.animes.Any(e => e.animeId == id);
        }



    }
}
