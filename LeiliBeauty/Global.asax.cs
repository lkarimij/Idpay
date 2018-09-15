using LeiliBeauty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace LeiliBeauty
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Dictionary<string, Order> ordersInMemory = new Dictionary<string, Order>();

            //System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application[AppConstants.OrdersInMemory] = ordersInMemory;
            //System.Web.HttpContext.Current.Application.UnLock();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["SetionID"] = Session.SessionID;
        }

        protected void Session_End(object sender, EventArgs e)
        {
            var ordersInMemory = (Dictionary<string, Order>)Application[AppConstants.OrdersInMemory];
            ordersInMemory.Remove(GetSession().SessionID);
            Application[AppConstants.OrdersInMemory] = ordersInMemory;
        }

        public HttpSessionState GetSession()
        {
            //Check if current context exists
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Session;
            }
            else
            {
                return this.Session;
            }
        }

        //protected void Application_EndRequest()
        //{
        //    if (Context.Response.StatusCode == 302)
        //    {
        //        Response.Clear();

        //        var rd = new RouteData();
        //        rd.DataTokens["area"] = "AreaName"; // In case controller is in another area
        //        rd.Values["controller"] = "Errors";
        //        rd.Values["action"] = "NotFound";

        //        IController c = new LeiliBeauty.Controllers.ErrorsController();
        //        c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
        //    }
        //    else if (Context.Response.StatusCode == 404)
        //    {
        //        Response.Clear();

        //        var rd = new RouteData();
        //        rd.DataTokens["area"] = "AreaName"; // In case controller is in another area
        //        rd.Values["controller"] = "Errors";
        //        rd.Values["action"] = "NotFound";

        //        IController c = new LeiliBeauty.Controllers.ErrorsController();
        //        c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
        //    }
        //}
    }
}
