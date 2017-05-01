using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Core_Server.Models.Data;
using System.Collections.Generic;

namespace Core_Server.Controllers
{
    public class SubCategoriesController : Controller
    {
        private readonly DataContext db;

        public SubCategoriesController(DataContext context) {
            db = context;
        }

        [HttpGet("/api/subcategories/{id}")]
        public IEnumerable<SubCategory> SubCategories(string id) {
            return db.SubCategories.Where(p => p.CategoryID == id).Join(db.Images, c => c.ImageID, i => i.Id, (c, i) => new { c, i}).AsEnumerable().Select(sc => { sc.c.Image = sc.i; return sc.c; });
        }

        [HttpGet("/api/subcategory/{id}")]
        public SubCategory SubCategory(string id) {
            return db.SubCategories.Where(p => p.Id == id).Join(db.Images, c => c.ImageID, i => i.Id, (c, i) => new { c, i}).AsEnumerable().Select(sc => { sc.c.Image = sc.i; return sc.c; }).First();
        }
    }
}