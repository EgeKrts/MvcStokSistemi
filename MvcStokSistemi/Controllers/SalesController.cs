using MvcStokSistemi.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStokSistemi.Controllers
{
    public class SalesController : Controller
    {
        
        DbMvcStokEntities db = new DbMvcStokEntities();
        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.Tbl_Sales.ToList();
            return View("Index",sales);
        }

        [HttpGet]
        public ActionResult NewSales()
        {
            //Products
            List<SelectListItem> product = (from i in db.Tbl_Product.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.Name,
                                                   Value = i.Id.ToString()
                                               }).ToList();

            ViewBag.dropProduct = product;

          
            //Staff
            List<SelectListItem> staff = (from i in db.Tbl_Staff.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.Name +" "+ i.Surname,
                                                Value = i.Id.ToString()
                                            }).ToList();

            ViewBag.dropStaff = staff;

            //Customers
            List<SelectListItem> customer = (from i in db.Tbl_Customer.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.Name +" "+ i.Surname,
                                                Value = i.Id.ToString()
                                            }).ToList();

            ViewBag.dropCustomer = customer;

            return View();
        }
        [HttpPost]
        public ActionResult NewSales(Tbl_Sales s)
        {
            var product = db.Tbl_Product.Where(m => m.Id == s.Tbl_Product.Id).FirstOrDefault();
            var staff = db.Tbl_Staff.Where(m => m.Id == s.Tbl_Staff.Id).FirstOrDefault();
            var customer = db.Tbl_Customer.Where(m => m.Id == s.Tbl_Customer.Id).FirstOrDefault();
            s.Tbl_Product = product;
            s.Tbl_Staff = staff;
            s.Tbl_Customer = customer;
           
            s.Date= DateTime.Now;

            db.Tbl_Sales.Add(s);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}