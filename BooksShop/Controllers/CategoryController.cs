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
    public class CategoryController : ControllerBase
    {
        UserContext db;

        List<Category> categories = new List<Category>()
            {
                new Category() {CategoryType="Horror"},
                new Category() {CategoryType="Romance"},
                new Category() {CategoryType="Fantasy"},
                new Category() {CategoryType="History"}
        };

        public CategoryController(UserContext context)
        {
            db = context;

            if (!db.Categories.Any())
            {
                foreach (var category in categories)
                {
                    db.Categories.Add(category);
                }
                db.SaveChanges();
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await db.Categories.ToListAsync();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Category>> Post(Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            db.Categories.Add(category);
            await db.SaveChangesAsync();
            return Ok(category);
        }
    }
}
