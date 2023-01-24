using MvcStokSistemi.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace MvcStokSistemi.Controllers
{
    public class CustomerController : Controller
    {
        DbMvcStokEntities db = new DbMvcStokEntities();
        // GET: Customer
        public ActionResult Index(int page=1)
        {
            //var customer = db.Tbl_Customer.ToList();
            var customer = db.Tbl_Customer.Where(x=>x.Status==true).ToList().ToPagedList(page,5);
            
            return View("Index",customer);
        }

        [HttpGet]
        public ActionResult NewCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewCustomer(Tbl_Customer c)
        {
            if (!ModelState.IsValid)
            {
                return View("NewCustomer");
            }
            c.Status = true;
            db.Tbl_Customer.Add(c);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult DeleteCustomer(Tbl_Customer c)
        {
            var findCustomer = db.Tbl_Customer.Find(c.Id);
            findCustomer.Status = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetCustomer(int id)
        {
            var customer = db.Tbl_Customer.Find(id);
            return View("GetCustomer",customer);
        }

        public ActionResult UpdateCustomer(Tbl_Customer c)
        {
            var cust = db.Tbl_Customer.Find(c.Id);
            cust.Name= c.Name;
            cust.Surname = c.Surname;
            cust.City= c.City;
            cust.Budget= c.Budget;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}