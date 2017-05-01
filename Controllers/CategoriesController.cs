using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Core_Server.Models.Data;
using System.Collections.Generic;

namespace Core_Server.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DataContext db;

        public CategoriesController(DataContext context) {
            db = context;
        }

        [HttpGet("/api/categories")]
        public IEnumerable<Category> Categories(){
            var a = db.Categories.Join(db.Images, c => c.ImageID, i => i.Id, (c, i) => new { c, i }).AsEnumerable().Select(sc => { sc.c.Image = sc.i; return sc.c; });

            return a;
        }

        [HttpGetAttribute("api/category/{id}")]
        public Category Category (string id) {
            var a = db.Categories.Where(p => p.Id == id).Join(db.Images, c => c.ImageID, i => i.Id, (c, i) => new { c, i }).AsEnumerable().Select(sc => { sc.c.Image = sc.i; return sc.c; }).First();
            return a;
        }
    }
}