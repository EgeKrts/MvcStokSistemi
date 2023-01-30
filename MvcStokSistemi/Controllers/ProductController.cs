using MvcStokSistemi.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStokSistemi.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Index(string p)
        {
            //var product = db.Tbl_Product.Where(x => x.Status == true).ToList();

            var products = db.Tbl_Product.Where(x =>x.Status == true);
            if (!string.IsNullOrEmpty(p))
            {
                products = products.Where(x => x.Name.Contains(p));
            }
            return View(products.ToList());
        }

        [HttpGet]
        public ActionResult NewProduct()
        {
            List<SelectListItem> categories = (from i in db.Tbl_Category.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.Name,
                                                   Value = i.Id.ToString()
                                               }).ToList();

            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public ActionResult NewProduct(Tbl_Product p)
        {
            var cat = db.Tbl_Category.Where(m => m.Id == p.Tbl_Category.Id).FirstOrDefault();
            p.Tbl_Category = cat;
            db.Tbl_Product.Add(p);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult GetProduct(int id)
        {
            List<SelectListItem> categories = (from i in db.Tbl_Category.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.Name,
                                                   Value = i.Id.ToString()
                                               }).ToList();

            ViewBag.Categories = categories;

            var product = db.Tbl_Product.Find(id);
            return View("GetProduct", product);
        }

        public ActionResult UpdateProduct(Tbl_Product p)
        {
            var product = db.Tbl_Product.Find(p.Id);
            product.Name = p.Name;
            product.Brand = p.Brand;
            product.Stock = p.Stock;
            product.PurchasePrice = p.PurchasePrice;
            product.SellingPrice = p.SellingPrice;

            var category = db.Tbl_Category.Where(m => m.Id == p.Tbl_Category.Id).FirstOrDefault();
            product.Category = category.Id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteProduct(Tbl_Product p)
        {
            var findProduct = db.Tbl_Product.Find(p.Id);
            findProduct.Status = false;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}