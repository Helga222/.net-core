using BooksShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserContext db;
        public UserController(UserContext context)
        {
            db = context;
            //if (!db.userBooks.Any())
            //{
            //    db.Add(new List<Book>());
            //    db.SaveChanges();
            //}
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<IEnumerable<Book>>> GetFavourites()
        {
            return await db.Books.Include(u => u.Category).Where(x=>x.Favourite==true).ToListAsync();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<Book>> AddToFavourites(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            Book book = db.Books.FirstOrDefault(x=>x.Id==id);
            if (book==null)
            {
                return NotFound();
            }
            book.Favourite = !book.Favourite;
            db.Update(book);
            await db.SaveChangesAsync();
            return Ok(book);
        }
}
}
