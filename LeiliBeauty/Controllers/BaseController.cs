using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeiliBeauty
{
    public abstract class BaseController : Controller
    {
        public ActionResult Error(string errorMessage, string controller = "", string action = "")
        {
            

            Models.ErrorModel model = new Models.ErrorModel();
            model.ErrorMessage = errorMessage;
            model.Controller = controller;
            model.Action = action;
            return View("Error", model);
        }

        public ActionResult Update(string controller = "", string action = "")
        {
            Models.UpdateModel model = new Models.UpdateModel();
            model.Controller = controller;
            model.Action = action;
            return View(model);
        }
    }
}