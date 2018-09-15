using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeiliBeauty.Controllers
{
    public class ErrorsController : BaseController
    {
        public string Index()
        {
            return "";
        }
        public ActionResult NotFound(string aspxerrorpath)
        {

            string errorMessage = string.Format("PathAndQuery: {0}, errorpath: {1}", Request.Url.PathAndQuery, aspxerrorpath);

            return Error(errorMessage, this.GetType().Name, "Error");
            //ActionResult result;

            //object model = Request.Url.PathAndQuery;

            //if (!Request.IsAjaxRequest())
            //    result = View(model);
            //else
            //    result = PartialView("_NotFound", model);

            //return result;
        }
    }
}
