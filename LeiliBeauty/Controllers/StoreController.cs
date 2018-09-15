using LeiliBeauty.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeiliBeauty.Controllers
{
    public class StoreController : BaseController
    {
        // GET: Store
        public ActionResult Index(string filter = null, int? type = null)
        {
            try
            {
                var storeMOdel = new StoreViewModels { Filter = filter, Products = StoreViewModels.GetStoreProducts(filter, type) };
                return View(storeMOdel);
            }
            catch (Exception ex)
            {
                return Error(ex.ToLogString(""), this.GetType().Name, "Register");
            }
        }




    }
}