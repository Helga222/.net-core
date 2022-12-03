using BooksShop.Models;
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
    public class UserController : ControllerBase
    {
        UserContext db;
        List<Book> books = new List<Book>()
            {
                new Book() {Name="The Shining", Author="Stephen King",
                            Category = Category.Horror,Cover = CoverType.Hard, Description="BestSeller",Pages=324},
                new Book() {Name="Pride and Prejudice", Author="Jane Austen",
                            Category = Category.Romance,Cover = CoverType.Paper, Description="Beautiful",Pages=512},
                new Book() {Name="Drakula", Author="Bram Stoker",
                            Category = Category.Horror,Cover = CoverType.Hard, Description="Wow",Pages=402},
                new Book() {Name="The Lord of the Rings", Author="J.R.R. Tolkien",
                            Category = Category.Fantasy,Cover = CoverType.Hard, Description="Wow!",Pages=1552},
                new Book() {Name="Drakula", Author="Bram Stoker",
                            Category = Category.Horror,Cover = CoverType.Hard, Description="Wow",Pages=402},
                new Book() {Name="John Adams", Author="David McCullough",
                            Category = Category.History,Cover = CoverType.Hard, Description="Wow",Pages=742}
            };

        public UserController(UserContext context)
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
            return await db.Books.ToListAsync();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            Book book = await db.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
                return NotFound();
            return new ObjectResult(book);
        }

        // POST api/users
        [HttpPost]
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

        // PUT api/users/
        [HttpPut]
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

        // DELETE api/users/5
        [HttpDelete("{id}")]
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