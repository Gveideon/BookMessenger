using BookMessenger.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMessenger.Controllers
{
    [Route("api/query")]
    public class RestController : Controller
    {
        ApplicationContext db;
        public RestController(ApplicationContext db)
        {
            this.db = db;
        }
        [Produces("application/json")]
        [HttpGet("searchBook")]
        
        public async Task<IActionResult> SearchBook()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = db.Books.Where(p => p.Title.Contains(term)).Select(p => p.Title).ToList();
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }
        [Produces("application/json")]
        [HttpGet("searchAuthor")]
        public async Task<IActionResult> SearchAuthor()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = db.Authors.Where(p => p.Name.Contains(term)).Select(p => p.Name).ToList();
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }
        [Produces("application/json")]
        [HttpGet("searchGenre")]
        public async Task<IActionResult> SearchGenre()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = db.Genres.Where(p => p.Name.Contains(term)).Select(p => p.Name).ToList();
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
