using MvcStokSistemi.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcStokSistemi.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Index()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Index(Tbl_Admin t)
        {
            var info = db.Tbl_Admin.FirstOrDefault(x=>x.Username == t.Username && x.Password == t.Password);
            if(info != null)
            {
                FormsAuthentication.SetAuthCookie(info.Username, false);
                return RedirectToAction("Index", "Customer");
            }
            else
            {
                return View();
            }
           
        }
    }
}