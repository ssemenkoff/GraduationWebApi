using Microsoft.AspNetCore.Mvc;
using Core_Server.Models.Data;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;

namespace Core_Server.Controllers
{
    [AuthorizeAttribute]
    public class OrdersController : Controller
    {
        private readonly DataContext db;

        public OrdersController(DataContext context) {
            db = context;
        }

        [AuthorizeAttribute(Roles="Admin")]
        [HttpGetAttribute("/api/orders")]
        public string Test() {
            return "Worked";
        }

        [AuthorizeAttribute(Roles="User")]
        [HttpPostAttribute("/api/order")]
        public void PostOrder(Models.Client.Data.Order order) {
            Models.Data.Order serverOrder = new Models.Data.Order();

            serverOrder.UserName = order.UserName;
            
            serverOrder.Accepted = false;
            serverOrder.Completed = false;

            serverOrder.Products = new HashSet<Models.Data.OrderProduct>();

            foreach (var productID in order.Products) {
                var orderProduct = new OrderProduct();
                orderProduct.Order = serverOrder;
                orderProduct.Product = db.Products.FirstOrDefault(x => x.Id == productID);

                serverOrder.Products.Add(orderProduct);
            }

            db.SaveChanges();
        }
    }
}