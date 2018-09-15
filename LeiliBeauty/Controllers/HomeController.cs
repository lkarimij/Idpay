using LeiliBeauty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LeiliBeauty.Controllers
{
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(GetHomeModel());
        }

        private HomeViewModels GetHomeModel()
        {


            return new HomeViewModels
            {
             
            };
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Cooperation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult HelpOrder()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult CooperationContact()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CooperationContact(EmailFormModel model)
        {
            try
            {
                var response = Request["g-recaptcha-response"];
                string secretKey = "6Lf4U1wUAAAAAI1_REd-yrvQmsyWlFzKoyPjlw5P";
                var client = new WebClient();
                var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
                var obj = JObject.Parse(result);
                var status = (bool)obj.SelectToken("success");

                if (!status)
                {
                    return Error("Google reCaptcha validation failed", "Home", "Index");
                }

                if (ModelState.IsValid)
                {
                    if (await Utils.SendEmail(model))
                        return RedirectToAction("Sent");
                }

                return Error("Parameter is not correct", this.GetType().Name, "Index");
            }
            catch (Exception ex)
            {
                return Error(ex.ToLogString("Contact"), this.GetType().Name, "Index");
            }

        }
    }
}