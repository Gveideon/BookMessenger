using BookMessenger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMessenger.Controllers
{
    public class GenreController : Controller
    {
        ApplicationContext db;
        public GenreController(ApplicationContext db)
        {
            this.db = db;
        }
        public IActionResult Index(int? id = 1)
        {
            var genres = db.Genres.ToList();
            var selectedGenre = genres.FirstOrDefault(b => b.Id == id);
            return View((genres, selectedGenre));
        }
        public IActionResult ShowGenreDescription(int? id)
        {
            var _selectedGenre = db.Genres.FirstOrDefault(b => b.Id == id);
            return RedirectToAction("Index", "Genre", new { id = _selectedGenre.Id });
        }
        [HttpPost]
        public IActionResult DeleteGenre(int? id)
        {
            if (id != null)
            {
                var g = db.Genres.FirstOrDefault(g => g.Id == id);
                if (g != null)
                {
                    db.Genres.Remove(g);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", new { });
        }

        public IActionResult EditGenre(int? id)
        {
            if (id != null)
            {
                var g = db.Genres.FirstOrDefault(g => g.Id == id);
                if (g != null)
                    return View(g);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult EditGenre(Genre genre)
        {
            if (genre != null)
            {
                db.Genres.Update(genre);
                db.SaveChanges();
                return RedirectToAction("Index", new { });

            }
            return NotFound();
        }
        public IActionResult CreateGenre()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateGenre(Genre genre)
        {
            db.Genres.Add(genre);
            db.SaveChanges();
            return RedirectToAction("Index", new { });
        }
    }
}
