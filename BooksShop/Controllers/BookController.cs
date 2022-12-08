using BooksShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        UserContext db;
        List<Book> books = new List<Book>()
            {
                new Book() {Name="The Shining", Author="Stephen King",
                            CategoryId = 1, Cover = "the_shining.jpg", Description="BestSeller",Pages=324},
                new Book() {Name="Pride and Prejudice", Author="Jane Austen",
                            CategoryId = 2, Cover = "pride_and_prej.jpg", Description="Beautiful",Pages=512},
                new Book() {Name="Drakula", Author="Bram Stoker",
                            CategoryId = 1, Cover = "drakula.jpg", Description="Wow",Pages=402},
                new Book() {Name="The Lord of the Rings", Author="J.R.R. Tolkien",
                            CategoryId = 3, Cover = "rings.jpg", Description="Wow!",Pages=1552},
                new Book() {Name="Drakula", Author="Bram Stoker",
                            CategoryId = 1, Cover = "drakula.jpg", Description="Wow",Pages=402},
                new Book() {Name="John Adams", Author="David McCullough",
                            CategoryId = 4, Cover = "john_addams.jpg", Description="Wow",Pages=742}
            };

        public BookController(UserContext context)
        {
            db = context;

            if (!db.Books.Any())
            {
                foreach(var book in books)
                {
                    db.Books.Add(book);
                }
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            return await db.Books.Include(u => u.Category).ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            Book book = await db.Books.Include(u => u.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
                return NotFound();
            return new ObjectResult(book);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Book>> Post(Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            db.Books.Add(book);
            await db.SaveChangesAsync();
            return Ok(book);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Book>> Put(Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            if (!db.Books.Any(x => x.Id == book.Id))
            {
                return NotFound();
            }

            db.Update(book);
            await db.SaveChangesAsync();
            return Ok(book);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Book>> Delete(int id)
        {
            Book book = db.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            db.Books.Remove(book);
            await db.SaveChangesAsync();
            return Ok(book);
        }
    }
}
