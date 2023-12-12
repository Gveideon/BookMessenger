﻿using BookMessenger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookMessenger.Controllers
{
    public class BookController : Controller
    {
        ApplicationContext db;
        private Book createdBook;
        public BookController(ApplicationContext db)
        {
            this.db = db;
        }
        public IActionResult Index(int? id=1)
        {
            db.Authors.Load();
            db.AuthorBooks.Load();
            db.Genres.Load();
            ViewBag.Genres = db.Genres.ToList();
            var books = db.Books.Include(b => b.Genres).ToList();
            var selectedBook = books.FirstOrDefault(b => b.Id == id);
            return View((books, selectedBook));
        }
        public IActionResult ShowBookDescription(int? id)
        {
            var _selectedBook = db.Books.FirstOrDefault(b => b.Id == id);
            return RedirectToAction("Index","Book", new { id = _selectedBook.Id });
        }
        [HttpPost]
        public IActionResult DeleteBook(int? id)
        {
            if(id != null)
            {
                var b = db.Books.FirstOrDefault(b => b.Id == id);
                if(b != null)
                {
                    db.Books.Remove(b);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", new { });
        }
        public IActionResult EditBook(int? id)
        {
            ViewBag.Genres = db.Genres.ToList();
            if (id != null)
            {
                var b = db.Books.FirstOrDefault(b => b.Id == id);
                if(b != null)
                    return View(b);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult EditBook(Book book, List<string> genres)
        {
            if (book != null)
            {
                var gl = db.Genres.ToList();
                foreach (var genre in genres)
                {
                    var g = gl.FirstOrDefault(g => g.Name == genre);
                    if(g is not  null)
                    {
                        book.Genres.Add(g);
                    }
                }
                db.Books.Update(book);
                db.SaveChanges();
                return RedirectToAction("Index", new { });

            }
            return NotFound();
        }
        public IActionResult CreateBook()
        {
            ViewBag.Genres = db.Genres.ToList();
            return View(ViewBag.createdBook);
        }
        [HttpPost]
        public IActionResult CreateBook(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
            return RedirectToAction("Index", new { });
        }
        public IActionResult AddAuthorToBook(int? bookId)
        {
            if (bookId != null)
            {
                var b = db.Books.FirstOrDefault(b => b.Id == bookId);

                var authorBook = new AuthorBook { Book = b };
                return View(authorBook);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult AddAuthorToBook(string? name,int? numberAuthor, int? id)
        {
            if (name != null && id != null)
            {
                var author = db.Authors.FirstOrDefault(a => a.Name == name);
                var book = db.Books.FirstOrDefault(a => a.Id == id);
                if (book != null && author != null)
                {
                    var ab = db.AuthorBooks.
                        FirstOrDefault(a => a.BookId == book.Id && a.AuthorId == author.Id);
                    if (ab is null)
                    {
                        if(numberAuthor is null) { numberAuthor = 0; }
                        author.AuthorBooks.Add(
                        new AuthorBook
                        {
                            Author = author,
                            Book = book,
                            AuthorId = author.Id,
                            BookId = book.Id,
                            NumberOfAuthor = (int)numberAuthor
                        });
                        db.SaveChanges();
                        return RedirectToAction("Index", new { });
                    }
                }

            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult DeleteAuthorFromBook(int? bookId, int? authorId)
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
        public IActionResult AddGenreToBook(int? bookId)
        {
            if (bookId != null)
            {
                var b = db.Books.FirstOrDefault(b => b.Id == bookId);
                return View(b);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult AddGenreToBook(string? name, int? id)
        {
            db.Books.Load();
            db.Genres.Load();
            var book = db.Books.Include( b => b.Genres).FirstOrDefault(b => b.Id == id);
            var genre = db.Genres.Include(g => g.Books).FirstOrDefault(g => g.Name == name);
            if (book != null &&
                genre != null &&
                book.Genres.FirstOrDefault( g => g.Id ==genre.Id) is null &&
                genre.Books.FirstOrDefault(b => b.Id == book.Id)  is null)
            {
                
                book.Genres.Add(genre);
                genre.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult DeleteGenreFromBook(int? bookId, int? genreId)
        {
            if (bookId != null && genreId != null)
            {
                var genre = db.Genres.Include(G => G.Books).FirstOrDefault(a => a.Id == genreId);
                var book = db.Books.Include(b => b.Genres).FirstOrDefault(a => a.Id == bookId);
                if (book != null && genre != null)
                {
                    genre.Books.Remove(book);
                    book.Genres.Remove(genre);
                    db.SaveChanges();
                    
                    return RedirectToAction("Index", new { });
                }
            }
            return BadRequest();
        }
    }
}
