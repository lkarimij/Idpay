using LeiliBeauty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeiliBeauty.Controllers
{
    public class BasketController : Controller
    {
        // GET: Basket
        public ActionResult Index(Int64 code, string name, bool inCatalog, int price, string imageUrl)
        {
            Order order = (Order)Session[SessionConstants.Basket];
            if (order == null)
                order = new Order();

            List<OrderDetail> orderDetails = order.OrderDetails;

            OrderDetail orderDetail = orderDetails.FirstOrDefault(d => d.ProductCode == code);

            if (orderDetail == null)
            {
                orderDetail = new OrderDetail()
                {
                    Name = name,

                    Count = 1,
                    ProductCode = code,
                    Price = price,
                    ImageUrl = imageUrl
                };
                orderDetails.Add(orderDetail);

            }
            order.Address = "آدرس";
            Session[SessionConstants.Basket] = order;
            return View(order);
        }

        [HttpGet]
        [Route("~/Basket/Delete/{code:int}")]
        public ActionResult Delete(int code)
        {
            return View();
        }


        [HttpPost]
        public ActionResult EditBasket(Order order)
        {
            return null;

        }
    }


}