using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Core_Server.Models.Data;
using System.Collections.Generic;

namespace Core_Server.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataContext db;

        public ProductsController(DataContext context) {
            db = context;
        }

        [HttpGet("/api/products")]
        public IEnumerable<Product> Products() {
            List<Product> result =  db.Products.Join(db.Images, p => p.PreviewImageId, i => i.Id, (c, i) => new { c, i}).AsEnumerable().Select(sc => { sc.c.PreviewImage = sc.i; return sc.c; }).ToList();

            result.ForEach((product) => {
                product.Images = db.Images.Where(i => i.Product == product).ToList();
            });

            return result;
        }

        [HttpGet("/api/product/{id}")]
        public Product Product(string id) {
            Product result =  db.Products.Where(p => p.Id == id).Join(db.Images, p => p.PreviewImageId, i => i.Id, (c, i) => new { c, i}).AsEnumerable().Select(sc => { sc.c.PreviewImage = sc.i; return sc.c; }).First();

            result.Images = db.Images.Where(i => i.Product == result).ToList();

            return result;
        }

        [HttpGet("/api/productsByCategory/{id}")]
        public IEnumerable<Product> ProductsByCategory(string id) {
            List<Product> result =  db.Products.Where(p => p.CategoryId == id).Join(db.Images, p => p.PreviewImageId, i => i.Id, (c, i) => new { c, i}).AsEnumerable().Select(sc => { sc.c.PreviewImage = sc.i; return sc.c; }).ToList();

            result.ForEach((product) => {
                product.Images = db.Images.Where(i => i.Product == product).ToList();
            });

            return result;
        }

        [HttpGet("/api/productsBySubcategory/{id}")]
        public IEnumerable<Product> productsBySubcategory(string id) {
            List<Product> result =  db.Products.Where(p => p.Id == id).Join(db.Images, p => p.PreviewImageId, i => i.Id, (c, i) => new { c, i}).AsEnumerable().Select(sc => { sc.c.PreviewImage = sc.i; return sc.c; }).ToList();

            result.ForEach((product) => {
                product.Images = db.Images.Where(i => i.Product == product).ToList();
            });

            return result;
        }
    }
}