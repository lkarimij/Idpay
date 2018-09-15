using LeiliBeauty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LeiliBeauty.Controllers
{
    public class OrderController : BaseController
    {
        [HttpGet]
        public ActionResult Index(long id)
        {
            Order order = Utils.CurrentOrder;
            if (order == null)
            {
                order = new Order();
                order.OrderDetails.Add(new OrderDetail(10000));
            }

            if (id != 0)
            {
                List<OrderDetail> orderDetails = order.OrderDetails;

                OrderDetail orderDetail = orderDetails.FirstOrDefault(d => d.ProductCode == id);

                if (orderDetail == null)
                {
                    orderDetail = new OrderDetail(id);

                    orderDetails.Add(orderDetail);
                }
                Utils.CurrentOrder = order;
            }

            return View(order);
        }

        [HttpPost]
        public ActionResult Index([Bind] Order order, string submitButton)
        {
            try
            {
                if (submitButton == "درگاه پرداخت")
                {
                    try
                    {
                        string id;
                        IdPay.callAPI(order.OrderDetails.Sum(d => (int)d.Price), string.Format("تعداد محصول سفارش داده شده: {0}", order.OrderDetails.Sum(d => d.Count)), out id);
                        //this id will be useful exactly when idPay send payment result so you must save it
                    }
                    catch (Exception ex)
                    {
                        //todo: save this error for reporting to Idpay
                    }

                    Utils.CurrentOrder = null;

                    return null;
                }
                else
                {
                    var productCode = Regex.Match(submitButton, @"حذف \((?<ProductCode>\d+)\)").Groups["ProductCode"].Value;
                    order.OrderDetails.RemoveAll(d => d.ProductCode == long.Parse(productCode));
                    Utils.CurrentOrder = order;
                    return View(order);
                }
            }
            catch (Exception ex)
            {
                return Error(ex.ToLogString(""), this.GetType().Name, "Index");
            }
        }

      
        [HttpPost]
        public ActionResult returnpayment(int status = 0, int track_id = 0, string id = "", string order_id = "", int amount = 0)
        {
            try
            {
                PayResultViewModel payModel = new PayResultViewModel();
                payModel.TetstResult = string.Format("status: '{1}', track_id: '{2}', id: '{3}', order_id: '{4}', amount: '{5}'", Environment.NewLine, status, track_id, id, order_id, amount);

                //find your order in dataBase base on pay_id (the id that you get of id pay and saved in database) 
                //save payment information in your dataBase for future follow up 

                //webOrder.PayID = id;
                //webOrder.PayAmount = amount;
                //webOrder.PayStatus = status;
                //webOrder.PayTrack_id = track_id;
                //webOrder.PayOrder_id = order_id;

                payModel.TrackingCode = track_id;
                switch (status)
                {
                    case 1:
                        payModel.PayComment = string.Format("پرداخت شما با موفقیت انجام نشد، جهت ادامه مسیر پرداخت با شما تماس گرفته خواهد شد");
                        payModel.PayStatus = "پرداخت انجام نشده است";
                        break;
                    case 2:
                        payModel.PayComment = string.Format("پرداخت شما با موفقیت انجام نشد، جهت ادامه مسیر پرداخت با شما تماس گرفته خواهد شد");
                        payModel.PayStatus = "پرداخت ناموفق بوده است";
                        break;
                    case 3:
                        payModel.PayComment = string.Format("پرداخت شما با موفقیت انجام نشد، جهت ادامه مسیر پرداخت با شما تماس گرفته خواهد شد");
                        payModel.PayStatus = "	خطا رخ داده است";
                        break;

                    case 100:
                        payModel.PayComment = string.Format(@"پرداخت شما با موفقیت انجام شد، به محض ارسال محصولات با شما تماس گرفته خواهد شد");
                        payModel.PayStatus = "پرداخت با موفقیت انجام شد";
                        break;

                    default:
                        break;
                }
                return View(payModel);
            }
            catch (Exception ex)
            {
                return Error(ex.ToLogString(""), this.GetType().Name, "Index");
            }

        }

    }
}