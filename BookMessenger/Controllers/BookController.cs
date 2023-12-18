using BookMessenger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
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
        public IActionResult Catalog(int? bookId,int? userId,
                string? bookName, string? authorName, int page = 1,
                SortBookState sortOrder = SortBookState.NameAsc)
        {
            int pageSize = 5;
            Book? selectedBook = null;
            UserProfile? user = null;
            db.Books.Load();
            db.Marks.Load();
            db.UserProfiles.Load();
            db.Genres.Load();
            db.AuthorBooks.Load();
            db.Authors.Load();
            if(bookId is not null) 
                selectedBook = db.Books.
                    Include(b => b.Genres).
                    Include(b => b.AuthorBooks).
                    Include(b => b.UserProfiles)
                    .FirstOrDefault(b => b.Id == bookId);
            if(userId is not null)
                user = db.UserProfiles.
                    Include(u => u.UserBooks).
                    Include(u => u.Books).
                    FirstOrDefault(b => b.UserId == userId);
            var books = db.Books.
                    Include(b => b.Genres).
                    Include(b => b.AuthorBooks).
                    Include(b => b.UserProfiles).ToList();
            if (HttpContext.Request.Cookies.ContainsKey("stateCatalog"))
            {
                var state = Request.Cookies["stateCatalog"];
                if(state == "MyLibrary")
                {
                    books = user.UserBooks
                        .Where(ub => ub.HasInLibrary == true)
                        .Select(b => b.Book).ToList();
                        
                }
                if (state == "Favorites")
                {
                    books = user.UserBooks
                        .Where(ub => ub.MarkValue == 1)
                        .Select(b => b.Book).ToList();
                }
            }
            else
            {
                HttpContext.Response.Cookies.Append("stateCatalog", "base");
            }
            if (!string.IsNullOrEmpty(bookName))
            {
                books = books.Where(p => p.Title!.ToLower().Contains(bookName.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(authorName))
            {
                books = books.
                    Where(p => p.
                        AuthorBooks.
                        Select(a => a.Author?.Name)
                        .Where(n => n!.ToLower().Contains(authorName.ToLower()))
                        .Count()> 0).
                      ToList();
            }
            switch (sortOrder)
            {
                case SortBookState.NameDesc:
                    books = books.OrderByDescending(s => s.Title).ToList();
                    break;
                case SortBookState.NameAsc:
                    books = books.OrderBy(s => s.Title).ToList();
                    break;
                case SortBookState.LikeDesc:
                    books = books.OrderByDescending(s => s.UserBooks.Where(a => a.MarkValue ==1).Count()).ToList();
                    break;
                case SortBookState.LikeAsc:
                    books = books.OrderBy(s => s.UserBooks.Where(a => a.MarkValue == 1).Count()).ToList();
                    break;
                case SortBookState.DislikeDesc:
                    books = books.OrderByDescending(s => s.UserBooks.Where(a => a.MarkValue == 0).Count()).ToList();
                    break;
                case SortBookState.DislikeAsc:
                    books = books.OrderBy(s => s.UserBooks.Where(a => a.MarkValue == 0).Count()).ToList();
                    break;
                case SortBookState.AddingDesc:
                    books = books.OrderByDescending(s => s.UserBooks.Where(a => (bool)a.HasInLibrary).Count()).ToList();
                    break;
                case SortBookState.AddingAsc:
                    books = books.OrderBy(s => s.UserBooks.Where(a => (bool)a.HasInLibrary).Count()).ToList();
                    break;
                default:
                    books = books.OrderByDescending(s => s.Title).ToList();
                    break;
            }

            var count = books.Count();
            var items = books.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            if (selectedBook is null)
            selectedBook = items.FirstOrDefault();
            CatalogViewModel viewModel = new CatalogViewModel(
            items,
            selectedBook,
            user,
                new FilterBookViewModel(bookName, authorName),
                new BookSortViewModel(sortOrder),
                new CatalogPageViewModel(count, page, pageSize)
            );
            return View(viewModel);
        }
        public IActionResult ShowBookCatalogDescription(int? bookId, int? userId)
        {
            return RedirectToAction("Catalog","Book", new { bookId = bookId, userId = userId });
        }
        public IActionResult AddToLibrary(int? bookId, int? userId, int? ubId)
        {
            db.Books.Load();
            db.Marks.Load();
            db.UserProfiles.Load();
            if (bookId is not null && userId  is not null)
            {
                var user = db.UserProfiles.
                    Include(u => u.UserBooks).
                    FirstOrDefault(b => b.UserId == userId);
                var book = db.Books.
                    Include(b => b.UserProfiles)
                    .FirstOrDefault(b => b.Id == bookId);
                UserBook? ub = null;
                if (book is not null && user is not null) {
                    if (ubId is not null)
                        ub = db.Marks.FirstOrDefault(b => b.Id == ubId);
                    if (ub is null)
                    {
                        ub = new UserBook
                        {
                            BookId = book.Id,
                            UserProfileId = user.Id,
                            Book = book,
                            User = user,
                            HasInLibrary = true
                        };
                        user.UserBooks.Add(ub);
                        db.SaveChanges();
                        return RedirectToAction("Catalog", "Book", new { bookId = bookId, userId = userId });
                    }
                    ub.HasInLibrary = true;
                    db.Marks.Update(ub);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Catalog", "Book", new { bookId = bookId, userId = userId });
        }
        public IActionResult RemoveFromLibrary(int? bookId, int? userId, int? ubId)
        {
            db.Books.Load();
            db.Marks.Load();
            db.UserProfiles.Load();
            if (bookId is not null && userId is not null)
            {
                var user = db.UserProfiles.
                    Include(u => u.UserBooks).
                    FirstOrDefault(b => b.UserId == userId);
                var book = db.Books.
                    Include(b => b.UserProfiles)
                    .FirstOrDefault(b => b.Id == bookId);
                UserBook? ub = null;
                if (book is not null && user is not null)
                {
                    if (ubId is not null)
                        ub = db.Marks.FirstOrDefault(b => b.Id == ubId);
                    if (ub is null)
                    {
                        ub = new UserBook
                        {
                            BookId = book.Id,
                            UserProfileId = user.Id,
                            Book = book,
                            User = user,
                            HasInLibrary = false
                        };
                        user.UserBooks.Add(ub);
                        db.SaveChanges();
                        return RedirectToAction("Catalog", "Book", new { bookId = bookId, userId = userId });
                    }
                    ub.HasInLibrary = false;
                    db.Marks.Update(ub);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Catalog", "Book", new { bookId = bookId, userId = userId });
        }
        public IActionResult LikeBook(int? bookId, int? userId, int? ubId)
        {
            db.Books.Load();
            db.Marks.Load();
            db.UserProfiles.Load();
            if (bookId is not null && userId is not null)
            {
                var user = db.UserProfiles.
                    Include(u => u.UserBooks).
                    FirstOrDefault(b => b.UserId == userId);
                var book = db.Books.
                    Include(b => b.UserProfiles)
                    .FirstOrDefault(b => b.Id == bookId);
                UserBook? ub = null;
                if (book is not null && user is not null)
                {
                    if (ubId is not null)
                        ub = db.Marks.FirstOrDefault(b => b.Id == ubId);
                    if (ub is null)
                    {
                        ub = new UserBook
                        {
                            BookId = book.Id,
                            UserProfileId = user.Id,
                            Book = book,
                            User = user,
                            HasInLibrary = false,
                            MarkValue =1
                        };
                        user.UserBooks.Add(ub);
                        db.SaveChanges();
                        return RedirectToAction("Catalog", "Book", new { bookId = bookId, userId = userId });
                    }
                    ub.MarkValue =1;
                    db.Marks.Update(ub);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Catalog", "Book", new { bookId = bookId, userId = userId });
        }
        public IActionResult DislikeBook(int? bookId, int? userId, int? ubId)
        {
            db.Books.Load();
            db.Marks.Load();
            db.UserProfiles.Load();
            if (bookId is not null && userId is not null)
            {
                var user = db.UserProfiles.
                    Include(u => u.UserBooks).
                    FirstOrDefault(b => b.UserId == userId);
                var book = db.Books.
                    Include(b => b.UserProfiles)
                    .FirstOrDefault(b => b.Id == bookId);
                UserBook? ub = null;
                if (book is not null && user is not null)
                {
                    if (ubId is not null)
                        ub = db.Marks.FirstOrDefault(b => b.Id == ubId);
                    if (ub is null)
                    {
                        ub = new UserBook
                        {
                            BookId = book.Id,
                            UserProfileId = user.Id,
                            Book = book,
                            User = user,
                            HasInLibrary = false,
                            MarkValue = 0
                        };
                        user.UserBooks.Add(ub);
                        db.SaveChanges();
                        return RedirectToAction("Catalog", "Book", new { bookId = bookId, userId = userId });
                    }
                    ub.MarkValue = 0;
                    db.Marks.Update(ub);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Catalog", "Book", new { bookId = bookId, userId = userId });
        }
        public IActionResult CancelMarkBook(int? bookId, int? userId, int? ubId)
        {
            db.Books.Load();
            db.Marks.Load();
            db.UserProfiles.Load();
            if (bookId is not null && userId is not null)
            {
                var user = db.UserProfiles.
                    Include(u => u.UserBooks).
                    FirstOrDefault(b => b.UserId == userId);
                var book = db.Books.
                    Include(b => b.UserProfiles)
                    .FirstOrDefault(b => b.Id == bookId);
                UserBook? ub = null;
                if (book is not null && user is not null)
                {
                    if (ubId is not null)
                        ub = db.Marks.FirstOrDefault(b => b.Id == ubId);
                    if (ub is null)
                    {
                        ub = new UserBook
                        {
                            BookId = book.Id,
                            UserProfileId = user.Id,
                            Book = book,
                            User = user,
                            HasInLibrary = false,
                            MarkValue = -1
                        };
                        user.UserBooks.Add(ub);
                        db.SaveChanges();
                        return RedirectToAction("Catalog", "Book", new { bookId = bookId, userId = userId });
                    }
                    ub.MarkValue = -1;
                    db.Marks.Update(ub);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Catalog", "Book", new { bookId = bookId, userId = userId });
        }
        public IActionResult UserLibrary(int? bookId, int? userId)
        {
            if (HttpContext.Request.Cookies.ContainsKey("stateCatalog"))
            {
                HttpContext.Response.Cookies.Delete("stateCatalog");
                HttpContext.Response.Cookies.Append("stateCatalog", "MyLibrary");
            }
            else
            {
                HttpContext.Response.Cookies.Append("stateCatalog", "MyLibrary");
            }
            return RedirectToAction("Catalog", "Book", new { bookId = bookId, userId = userId });

        }
        public IActionResult UserFavorite(int? bookId, int? userId)
        {
            if (HttpContext.Request.Cookies.ContainsKey("stateCatalog"))
            {
                HttpContext.Response.Cookies.Delete("stateCatalog");
                HttpContext.Response.Cookies.Append("stateCatalog", "Favorites");
            }
            else
            {
                HttpContext.Response.Cookies.Append("stateCatalog", "Favorites");
            }
            return RedirectToAction("Catalog", "Book", new { bookId = bookId, userId = userId });

        }
        public IActionResult CatalogShow(int? bookId, int? userId)
        {
            if (HttpContext.Request.Cookies.ContainsKey("stateCatalog"))
            {
                HttpContext.Response.Cookies.Delete("stateCatalog");
                HttpContext.Response.Cookies.Append("stateCatalog", "base");
            }
            else
            {
                HttpContext.Response.Cookies.Append("stateCatalog", "base");
            }
            return RedirectToAction("Catalog", "Book", new { bookId = bookId, userId = userId });

        }
    }
}
