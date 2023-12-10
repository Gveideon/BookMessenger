using BookMessenger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMessenger.Controllers
{
    public class AuthorController : Controller
    {
        ApplicationContext db;
        public AuthorController(ApplicationContext db)
        {
            this.db = db;
        }
        public IActionResult Index(int? id = 1)
        {
            db.Books.Load();
            db.AuthorBooks.Load();
            var authors = db.Authors.ToList();
            var selectedAuthor = authors.FirstOrDefault(b => b.Id == id);
            return View((authors, selectedAuthor));
        }
        public IActionResult ShowAuthorDescription(int? id)
        {
            var _selectedAuthor = db.Authors.FirstOrDefault(b => b.Id == id);
            return RedirectToAction("Index", "Author", new { id = _selectedAuthor.Id });
        }
        [HttpPost]
        public IActionResult DeleteAuthor(int? id)
        {
            if (id != null)
            {
                var a = db.Authors.FirstOrDefault(a => a.Id == id);
                if (a != null)
                {
                    db.Authors.Remove(a);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", new { });
        }
        [HttpPost]
        public IActionResult DeleteBookFromAuthor(int? bookId, int? authorId)
        {
            if (bookId != null && authorId != null)
            {
                var author = db.Authors.FirstOrDefault(a => a.Id == authorId);
                var book = db.Books.FirstOrDefault(a => a.Id == bookId);
                if (book != null && author != null)
                {
                    var ab = db.AuthorBooks.
                        FirstOrDefault(a => a.BookId == book.Id && a.AuthorId == author.Id);
                    if (ab != null)
                    {
                        db.AuthorBooks.Remove(ab);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index", new { });
                }
            }
           return BadRequest();
        }

        public IActionResult EditAuthor(int? id)
        {
            if (id != null)
            {
                var a = db.Authors.FirstOrDefault(a => a.Id == id);
                if (a != null)
                    return View(a);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult EditAuthor(Author author)
        {
            if (author != null)
            {
                db.Authors.Update(author);
                db.SaveChanges();
                return RedirectToAction("Index", new { });

            }
            return NotFound();
        }
        public IActionResult CreateAuthor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAuthor(Author author)
        {
            db.Authors.Add(author);
            db.SaveChanges();
            return RedirectToAction("Index", new { });
        }
        public IActionResult AddBookToAuthor(int? authorId)
        {
            if (authorId != null)
            {
                var a = db.Authors.FirstOrDefault(author => author.Id == authorId);

                var authorBook = new AuthorBook { Author = a };
                return View(authorBook);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult AddBookToAuthor(string? title,int? id )
        {
            if(title != null && id != null)
            {
                var author = db.Authors.FirstOrDefault(a => a.Id == id);
                var book = db.Books.FirstOrDefault(a => a.Title == title); 
                if (book != null && author != null) {
                    var ab = db.AuthorBooks.
                        FirstOrDefault(a => a.BookId == book.Id && a.AuthorId == author.Id);
                    if (ab is null)
                    {
                        var maxNumber = db.AuthorBooks?.
                            Where(a => a.BookId == book.Id).
                            Select(a => a.NumberOfAuthor).
                            DefaultIfEmpty().
                            Max();
                        if (maxNumber is null) maxNumber = 1;
                        author.AuthorBooks.Add(
                        new AuthorBook
                        {
                            Author = author,
                            Book = book,
                            AuthorId = author.Id,
                            BookId = book.Id,
                            NumberOfAuthor = (int)maxNumber+1
                        });
                        db.SaveChanges();
                        return RedirectToAction("Index", new { });
                    }
                }
                
            }
           return BadRequest();
        }
    }
}
